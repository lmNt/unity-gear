using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class EZPrefs
{
   // Static constructor useful for version checking
   static EZPrefs() {
      if (Version != "1.5") {
         // Invoke upgrade process if necessary.
      }
   }

   public static string Version {
      get { return EZPrefs.Get("version", "1.0"); }
      set { EZPrefs.Set("version", value); }
   }
   public static int LastScore {
      get { return EZPrefs.Get<int>("last score"); }
      set { EZPrefs.Set("last score", value); }
   }
   public static int Highscore {
      get { return EZPrefs.Get<int>("highscore"); }
      set { EZPrefs.Set("highscore", value); }
   }
   public static bool MusicEnabled {
      get { return EZPrefs.Get<bool>("music enabled"); }
      set { EZPrefs.Set("music enabled", value); }
   }
   public static List<bool> LevelsCompleted {
      get { return EZPrefs.GetListPrefs<bool>("levels completed"); }
      set { EZPrefs.NewListPrefs("levels completed", value); }
   }

   private static void Set<T>(string key, T value) {
      if (typeof(T) == typeof(string))
         PlayerPrefs.SetString(key, value as string);
      else if (typeof(T) == typeof(int))
         PlayerPrefs.SetInt(key, Convert.ToInt32(value));
      else if (typeof(T) == typeof(bool))
         PlayerPrefs.SetInt(key, Convert.ToInt32(value));
      else if (typeof(T) == typeof(float))
         PlayerPrefs.SetFloat(key, (float)(object)value);
   }

   private static T Get<T>(string key, T default_value = default(T)) {
      if (!EZPrefs.Has(key)) return default_value;

      if (typeof(T) == typeof(string))
         return (T)Convert.ChangeType(PlayerPrefs.GetString(key), typeof(T));
      else if (typeof(T) == typeof(int))
         return (T)Convert.ChangeType(PlayerPrefs.GetInt(key), typeof(T));
      else if (typeof(T) == typeof(bool))
         return (T)Convert.ChangeType(PlayerPrefs.GetInt(key), typeof(T));
      else
         return (T)Convert.ChangeType(PlayerPrefs.GetFloat(key), typeof(T));
   }

   private static bool Has(string key) {
      return PlayerPrefs.HasKey(key);
   }

   private static List<T> GetListPrefs<T>(string key) {
      if (!EZPrefs.Has(key)) return null;

      int count = EZPrefs.Get<int>(key);
      List<T> list = new List<T>();
      for (int i = 0; i < count; i++) {
         list.Add(EZPrefs.Get<T>(key + i.ToString()));
      }
      return list;
   }

   private static void NewListPrefs<T>(string key, List<T> list, int offset = 0) {
      int i = offset;
      EZPrefs.Set<int>(key, list.Count);
      foreach (T pref in list) {
         EZPrefs.Set<T>(key + i.ToString(), pref);
         i++;
      }
   }

   private static void AppendListPrefs<T>(string key, List<T> list) {
      if (!EZPrefs.Has(key)) {
         EZPrefs.NewListPrefs<T>(key, list);
      }
      else {
         int count = EZPrefs.Get<int>(key);
         EZPrefs.Set<int>(key, count + list.Count);
         EZPrefs.NewListPrefs<T>(key, list, count);
      }
   }
}
