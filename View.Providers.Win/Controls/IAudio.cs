using System;

namespace Okorodudu.Checkers.View.Providers.Win.Controls
{
   /// <summary>
   /// The interface for playing audio
   /// </summary>
   public interface IAudio
   {
      /// <summary>
      /// Play the given wav resource
      /// </summary>
      /// <param name="wav">The wav resource</param>
      void PlayWavResource(string wav);

      /// <summary>
      /// Play the given wav resource without interrupting any audio currently being played
      /// </summary>
      /// <param name="wav">The wav resource</param>
      void PlayWavResourceYield(string wav);
   }
}
