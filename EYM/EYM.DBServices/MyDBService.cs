using EYM.Repositories.Interfaces;
using EYM.DBServices.Interfaces;
using System;

namespace EYM.DBServices
{
	class MyDBService : IMyDBService
	{
		IMyRepository _rep;
		public MyDBService(IMyRepository rep)
		{
			_rep = rep;
        }
		public void Serve()
		{
			_rep.Get();
        }
	}
}
