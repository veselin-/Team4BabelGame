
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace UnityTest
{
	[TestFixture]
	[Category("Smoke Tests")]
	internal class SmokeTests
	{
		[Test]
		public void RandomTest()
		{
			int random = Random.Range (0, 2);
			switch (random)
			{
			case 0:
				PassingTest();
				break;
			case 1:
				FailingTest();
				break;
			default:
				PassingTest();
				break;
			}

			PassingTest ();
		}


		public void PassingTest()
		{
			int one = 1;
			int two = 2;
			int result = one + two;
			Assert.AreEqual(3, result);
		}

	
		public void FailingTest()
		{
			int one = 1;
			int two = 2;
			int result = one + two;
			Assert.AreNotEqual(4, result);
		}
	}
}
