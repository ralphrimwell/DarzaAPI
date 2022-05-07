# DarzaAPI
Darza's Dominion API

Ripped directly from the beta build, but changed the public key

Only support for login (nothing else has been tested)

Example:
```cs
public static void SendRequest(string email, string password)
        {
			WebServerClient.Login(email, password, delegate (WbLoginResp response)
			{
				if (response == null)
				{
					Console.WriteLine("failure");
					Data.Checked++;
					Thread.Sleep(5000);
					return;
				}
				else if (response.Success)
				{
					Console.WriteLine("Correct credentials");
					Console.WriteLine("auth token: " + response.Value);
				}
				Console.WriteLine(response.Value);
				Data.Checked++;

			});
		}
```
