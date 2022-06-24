using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ContactoService
    {

        #region Define as Singleton
        private static ContactoService _Instance;

        public static ContactoService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ContactoService();
                }

                return (_Instance);
            }
        }

        private ContactoService()
        {
        }
        #endregion



        public List<Contacto> BuscarContacto(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var contac = context.Contactos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                contac = contac.Where(x => x.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            count = contac.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return contac.OrderByDescending(x => x.Email).Skip(skipCount).Take(recordSize).ToList();
        }

        public Contacto GetContactoByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Contactos.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveContacto(Contacto Contacto)
        {
            var context = DataContextHelper.GetNewContext();

            context.Contactos.Add(Contacto);

            return context.SaveChanges() > 0;
        }

        public bool DeleteContacto(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var contacto = context.Contactos.Find(ID);

            contacto.IsDeleted = true;

            context.Entry(contacto).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }



    }
}
