using System;
using EYM.AppServices.Interfaces;
using EYM.DBServices.Interfaces;

namespace EYM.AppServices
{
	public class MyService : IAppService
	{
		IMyDBService _dbService;
        public MyService(IMyDBService dbService)
		{
			_dbService = dbService;
        }
		public void Serve()
		{
			_dbService.Serve();
        }
	}
}
