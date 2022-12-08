using System;
using System.Threading.Tasks;

namespace Cappario
{
    public static class RetryHelper    
    {
        public static void RetryOnException(int times, TimeSpan delay, Action operation)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    operation();
                    break; // Sucess! Lets exit the loop!
                }
                catch (Exception ex)
                {
                    if (attempts == times)
                        throw ex;


                    Task.Delay(delay).Wait();
                }
            } while (true);
        }
    }
}