using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public static class Prefs
{
   // Static constructor useful for version checking
   static Prefs()
   {
      if (Version != "1.5")
      {
         // Invoke upgrade process if necessary.
      }
   }
   
   public static void Set<T>(string key, T value){
      if (typeof(T) == typeof(string))
        PlayerPrefs.SetString(key, value);
      else if (typeof(T) == typeof(int))
        PlayerPrefs.SetInt(key, value);
      else if (typeof(T) == typeof(bool))
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
      else if (typeof(T) == typeof(float))
        PlayerPrefs.SetFloat(key, value);
   }
    
    public static T Get<T>(string key){
      if (typeof(T) == typeof(string))
        return PlayerPrefs.GetString(key);
      else if (typeof(T) == typeof(int))
        return PlayerPrefs.GetInt(key);
      else if (typeof(T) == typeof(bool))
        return Convert.ToBoolean(PlayerPrefs.GetInt(key));
      else if (typeof(T) == typeof(float))
        return PlayerPrefs.GetFloat(key);
   }
 
   public static string Version
   {
      get { return PlayerPrefs.GetString("version", "1.0"); }
   }
 
   public static int LastScore
   {
      get { return PlayerPrefs.GetInt("last score", 0); }
      set { PlayerPrefs.SetInt("last score", value); }
   }
 
   public static bool MusicEnabled
   {
      get { return PlayerPrefs.GetInt("music enabled", 1) != 0; }
      set { PlayerPrefs.SetInt("music enabled", value != 0); }
   }
   
   private static List<T> GetListPrefs(string key){
    int count = Get(key);
    List<T> list = new List<T>;
    for (int i=0; i < count; i++){
     
    }
   }
   
   private static void NewListPrefs<T>(string key, List<T> list){
      int i=0;
      foreach (T pref in list){
        Set<T>(key + i.ToString(), pref);
        i++;
      }
   }
   
   private static void AppendListPrefs(string key, List<T> list){
      if (PlayerPrefs.HasKey(key)){
        NewListPrefs<T>(key, list);
      }
      else {
        int count = PlayerPrefs.GetInt(key);
        Set<T>(key, count+list.Count);
        foreach (T pref in list){
          Set<T>(key + count.ToString(), pref);
          count++;
        }
      }
   }
}
