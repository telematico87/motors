using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ColorService
    {

        #region Define as Singleton
        private static ColorService _Instance;

        public static ColorService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ColorService();
                }

                return (_Instance);
            }
        }

        private ColorService()
        {
        }
        #endregion



        public List<Color> GetAllColors()
        {
            var context = DataContextHelper.GetNewContext(); 
            var colors = context.Colors.ToList(); 
            return colors.ToList();
        }







        public List<Color> SearchColor(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var color = context.Colors
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                color = color.Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            count = color.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return color.OrderByDescending(x => x.Description).Skip(skipCount).Take(recordSize).ToList();
        }


        public Color GetColorByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Colors.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveColor(Color Color)
        {
            var context = DataContextHelper.GetNewContext();

            context.Colors.Add(Color);

            return context.SaveChanges() > 0;
        }

        public bool UpdateColor(Color color)
        {
            var context = DataContextHelper.GetNewContext();

            context.Entry(color).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteColor(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var colors = context.Colors.Find(ID);

            colors.IsDeleted = true;

            context.Entry(colors).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }



    }
}
