﻿// <copyright file="AndroidClient.cs" company="Google Inc.">
// Copyright (C) 2015 Google Inc.  All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// </copyright>

#if UNITY_ANDROID
namespace GooglePlayGames.Android
{
    using System;
    using UnityEngine;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames.OurUtils;
    using Com.Google.Android.Gms.Common.Api;
    using Com.Google.Android.Gms.Games.Stats;
    using Com.Google.Android.Gms.Games;
    using C = GooglePlayGames.Native.Cwrapper.InternalHooks;
    using GooglePlayGames.Native.PInvoke;


    internal class AndroidClient : IClientImpl
    {
        internal const string BridgeActivityClass = "com.google.games.bridge.NativeBridgeActivity";
        private const string LaunchBridgeMethod = "launchBridgeIntent";
        private const string LaunchBridgeSignature =
            "(Landroid/app/Activity;Landroid/content/Intent;)V";

        private TokenClient tokenClient;

        public PlatformConfiguration CreatePlatformConfiguration()
        {
            var config = AndroidPlatformConfiguration.Create();
            using (var activity = AndroidTokenClient.GetActivity())
            {
                config.SetActivity(activity.GetRawObject());
                config.SetOptionalIntentHandlerForUI((intent) =>
                    {
                        // Capture a global reference to the intent we are to show. This is required
                        // since we are launching the intent from the game thread, and this callback
                        // will return before this happens. If we do not hold onto a durable reference,
                        // the code calling us will clean up the intent before we have a chance to display
                        // it.
                        IntPtr intentRef = AndroidJNI.NewGlobalRef(intent);

                        PlayGamesHelperObject.RunOnGameThread(() =>
                            {
                                try
                                {
                                    LaunchBridgeIntent(intentRef);
                                }
                                finally
                                {
                                    // Now that we've launched the intent, release the global reference.
                                    AndroidJNI.DeleteGlobalRef(intentRef);
                                }
                            });
                    });
            }

            return config;
        }


        public TokenClient CreateTokenClient(bool reset)
        {
            if (tokenClient == null || reset)
            {
                tokenClient = new AndroidTokenClient();
            }
            return tokenClient;
        }


        // Must be launched from the game thread (otherwise the classloader cannot locate the unity
        // java classes we require).
        private static void LaunchBridgeIntent(IntPtr bridgedIntent)
        {
            object[] objectArray = new object[2];
            jvalue[] jArgs = AndroidJNIHelper.CreateJNIArgArray(objectArray);
            try
            {
                using (var bridgeClass = new AndroidJavaClass(BridgeActivityClass))
                {
                    using (var currentActivity = AndroidTokenClient.GetActivity())
                    {
                        // Unity no longer supports constructing an AndroidJavaObject using an IntPtr,
                        // so I have to manually munge with JNI here.
                        IntPtr methodId = AndroidJNI.GetStaticMethodID(bridgeClass.GetRawClass(),
                                              LaunchBridgeMethod,
                                              LaunchBridgeSignature);
                        jArgs[0].l = currentActivity.GetRawObject();
                        jArgs[1].l = bridgedIntent;
                        AndroidJNI.CallStaticVoidMethod(bridgeClass.GetRawClass(), methodId, jArgs);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.e("Exception launching bridge intent: " + e.Message);
                Logger.e(e.ToString());
            }
            finally
            {
                AndroidJNIHelper.DeleteJNIArgArray(objectArray, jArgs);
            }
        }

        public void GetPlayerStats(IntPtr apiClient, Action<CommonStatusCodes,
            PlayGamesLocalUser.PlayerStats> callback)
        {
            GoogleApiClient client = new GoogleApiClient(apiClient);
            StatsResultCallback resCallback;

            try
            {
                resCallback = new StatsResultCallback((result, stats) =>
                    {
                        Debug.Log("Result for getStats: " + result);
                        PlayGamesLocalUser.PlayerStats s = null;
                        if (stats != null)
                        {
                            s = new PlayGamesLocalUser.PlayerStats();
                            s.AvgSessonLength = stats.getAverageSessionLength();
                            s.DaysSinceLastPlayed = stats.getDaysSinceLastPlayed();
                            s.NumberOfPurchases = stats.getNumberOfPurchases();
                            s.NumOfSessions = stats.getNumberOfSessions();
                            s.SessPercentile = stats.getSessionPercentile();
                            s.SpendPercentile = stats.getSpendPercentile();
                            s.ChurnProbability = stats.getChurnProbability();
                        }
                        callback((CommonStatusCodes)result, s);
                    });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                callback(CommonStatusCodes.DeveloperError, null);
                return;
            }

            PendingResult<Stats_LoadPlayerStatsResultObject> pr =
                Games.Stats.loadPlayerStats(client, true);

            pr.setResultCallback(resCallback);
        }

        class StatsResultCallback : ResultCallbackProxy<Stats_LoadPlayerStatsResultObject>
        {
            private Action<int, PlayerStats> callback;

            public StatsResultCallback(Action<int, PlayerStats> callback)
            {
                this.callback = callback;
            }

            public override void OnResult(Stats_LoadPlayerStatsResultObject arg_Result_1)
            {
                callback(arg_Result_1.getStatus().getStatusCode(), arg_Result_1.getPlayerStats());
            }
        }
    }
}
#endif
