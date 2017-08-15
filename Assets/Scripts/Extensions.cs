using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


// Code below courtesy of user "grenade" from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp

// This class is used to generate random numbers that thread-safe
public static class ThreadSafeRandom {
	[ThreadStatic] private static System.Random Local;

	public static System.Random ThisThreadsRandom {
		get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
	}
}

// This class holds extension methods, including Shuffle() which randomizes the elements in a List
static class MyExtensions {
   	public static void Shuffle<T>(this IList<T> list) {
    	int n = list.Count;
    	while (n > 1) {
      	n--;
       	int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
       	T value = list[k];
       	list[k] = list[n];
       	list[n] = value;
    	}
	}
}