using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SR2.Hubs
{
	public class MyHub1 : Hub
	{
		public void Hello()
		{
			Timer t = new Timer((obj) =>
			{
				Clients.Caller.tick();

			}, null, 0, 1000);
		}
	}
}