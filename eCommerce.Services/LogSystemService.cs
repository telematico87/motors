using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class LogSystemService
    {
        #region Define as Singleton
        private static LogSystemService _Instance;

        public static LogSystemService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LogSystemService();
                }

                return (_Instance);
            }
        }
        public LogSystemService()
        {
        }
        #endregion

        public bool Save(string Mensaje)
        {
            var context = DataContextHelper.GetNewContext();

            LogSystem e = new LogSystem();
            e.LogDescription = Mensaje;
            e.LogDate = DateTime.Now;
            context.LogSystems.Add(e);
            return context.SaveChanges() > 0;
        }
    }
}
