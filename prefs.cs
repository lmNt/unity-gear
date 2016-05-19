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
   
   public static string Version
   {
      get { return Prefs.Get("version", "1.0"); }
      set { Prefs.Set("version", value); }
   }
 
   public static int LastScore
   {
      get { return Prefs.Get("last score"); }
      set { Prefs.Set("last score", value); }
   }
   public static int Highscore
   {
      get { return Prefs.Get("highscore"); }
      set { Prefs.Set("highscore", value); }
   }
 
   public static bool MusicEnabled
   {
      get { return Prefs.Get("music enabled"); }
      set { Prefs.Set("music enabled", value); }
   }
   
   private static void Set<T>(string key, T value){
      if (typeof(T) == typeof(string))
        PlayerPrefs.SetString(key, value);
      else if (typeof(T) == typeof(int))
        PlayerPrefs.SetInt(key, value);
      else if (typeof(T) == typeof(bool))
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
      else if (typeof(T) == typeof(float))
        PlayerPrefs.SetFloat(key, value);
   }
    
    private static T Get<T>(string key, T default_value = default(T)){
      if (!Prefs.Has(key)) return default_value;
      
      if (typeof(T) == typeof(string))
        return PlayerPrefs.GetString(key);
      else if (typeof(T) == typeof(int))
        return PlayerPrefs.GetInt(key);
      else if (typeof(T) == typeof(bool))
        return Convert.ToBoolean(PlayerPrefs.GetInt(key));
      else if (typeof(T) == typeof(float))
        return PlayerPrefs.GetFloat(key);
   }
   
   private static bool Has(string key){
    return PlayerPrefs.HasKey(key);
   }
 

   
   private static List<T> GetListPrefs(string key){
    if (!Prefs.Has(key)) return null;
    int count = Prefs.Get(key);
    List<T> list = new List<T>;
    for (int i=0; i < count; i++){
     list.Add(Prefs.Get<T>(key+i.ToString()));
    }
    return list;
   }
   
   private static void NewListPrefs<T>(string key, List<T> list, int offset = 0){
      int i=offset;
      foreach (T pref in list){
        Prefs.Set<T>(key + i.ToString(), pref);
        i++;
      }
   }
   
   private static void AppendListPrefs(string key, List<T> list){
      if (!Prefs.Has(key)){
        Prefs.NewListPrefs<T>(key, list);
      }
      else {
        int count = Prefs.Get(key);
        Prefs.Set<T>(key, count+list.Count);
        Prefs.NewListPrefs<T>(key, list, count);
      }
   }
}
