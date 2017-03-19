using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;

namespace Okorodudu.Checkers.View.Providers.Win.Controls
{
   /// <summary>
   /// Win 32 Audio player implementation
   /// </summary>
   public class Win32Audio : IAudio, IDisposable
   {
      private static Dictionary<string, string> cachedWavs = new Dictionary<string, string>();
      private static object cachedWavsLock = new object();
      private TempFileCollection tempFiles = new TempFileCollection();

      private static class NativeMethods
      {
         //[DllImport("winmm.dll")]
         //public static extern bool PlaySound(byte[] data, IntPtr hMod, uint dwFlags);

         [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
      }

      #region IAudio Members

      /// <summary>
      /// Play the given wav resource
      /// </summary>
      /// <param name="wav">The wav resource</param>
      public void PlayWavResource(string wav)
      {
         string wavFilePath = GetWavResource(wav);
         if (!string.IsNullOrEmpty(wavFilePath))
         {
            NativeMethods.PlaySound(wavFilePath, IntPtr.Zero, SoundFlags.SND_ASYNC);
         }
      }

      /// <summary>
      /// Play the given wav resource without interrupting any audio currently being played
      /// </summary>
      /// <param name="wav">The wav resource</param>
      public void PlayWavResourceYield(string wav)
      {
         string wavFilePath = GetWavResource(wav);
         if (!string.IsNullOrEmpty(wavFilePath))
         {
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            //player.SoundLocation = wavFilePath;
            //player.Play();
            NativeMethods.PlaySound(wavFilePath, IntPtr.Zero, SoundFlags.SND_ASYNC | SoundFlags.SND_NOSTOP);
         }
      }
      #endregion IAudio Members

      private string GetWavResource(string wav)
      {
         if (!cachedWavs.ContainsKey(wav))
         {
            lock (cachedWavsLock)
            {
               if (!cachedWavs.ContainsKey(wav))
               {
                  // get the namespace 
                  string wavResource = "Okorodudu.Checkers.View.Providers.Win.Resources.Audio." + wav;

                  // get the resource into a stream
                  using (Stream wavInputStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(wavResource))
                  {
                     string tempfile = System.IO.Path.GetTempFileName();
                     tempFiles.AddFile(tempfile, false);
                     byte[] wavData = new byte[wavInputStream.Length];
                     wavInputStream.Read(wavData, 0, (int)wavInputStream.Length);
                     File.WriteAllBytes(tempfile, wavData);
                     cachedWavs.Add(wav, tempfile);
                  }
               }
            }
         }

         string tempWavFile = (cachedWavs.ContainsKey(wav)) ? cachedWavs[wav] : string.Empty;
         return tempWavFile;
      }

      /// <summary>
      /// Dispose this object
      /// </summary>
      public void Dispose()
      {
         this.Dispose(true);
         System.GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Dispose this object
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources</param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            IDisposable disposableTempFiles = tempFiles as IDisposable;
            if (disposableTempFiles != null)
            {
               disposableTempFiles.Dispose();
            }
         }
      }

      private static class SoundFlags
      {
         //public static uint SND_SYNC = 0x0000;         // play synchronously (default)
         public static uint SND_ASYNC = 0x0001;        // play asynchronously
         //public static uint SND_NODEFAULT = 0x0002;    // silence (!default)if sound not found
         //public static uint SND_MEMORY = 0x0004;       // pszSound points to a memory file
         //public static uint SND_LOOP = 0x0008;         // loop the sound until next sndPlaySound
         public static uint SND_NOSTOP = 0x0010;       // don't stop any currently playing sound
         //public static uint SND_NOWAIT = 0x00002000;   // don't wait if the driver is busy
         //public static uint SND_ALIAS = 0x00010000;    // name is a Registry alias
         //public static uint SND_ALIAS_ID = 0x00110000; // alias is a predefined ID
         //public static uint SND_FILENAME = 0x00020000; // name is file name
         //public static uint SND_RESOURCE = 0x00040004; // name is resource name or atom
         //public static uint SND_PURGE = 0x0040;        // purge non-static events for task
         //public static uint SND_APPLICATION = 0x0080;  // look for application-specific association
      }
   }
}
