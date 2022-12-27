using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared;
using eCommerce.Shared.Commons;
using eCommerce.Shared.Enums;
using eCommerce.Shared.Extensions;
using eCommerce.Shared.Helpers;
using eCommerce.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Controllers
{
    public class HomeController : PublicBaseController
    {
        public ActionResult Index()
        {
            /* if multi language is enabled and the home page is accessed without any language, redirect to the default language */
            if (ConfigurationsHelper.EnableMultilingual && Request.Url.AbsolutePath.ToString().Equals("/"))
            {
                return Redirect(Url.Home());
            }

            return View(new PageViewModel());
        }

        //[OutputCache(Duration = 600)]
        public ActionResult HomeSliders()
        {
            HomeSlidersViewModel model = new HomeSlidersViewModel
            {
                SlidersConfigurations = ConfigurationsService.Instance.GetConfigurationsByType((int)ConfigurationTypes.Sliders)
            };
            return PartialView("_BannerSliderBm3", model);
        }
        
        public ActionResult HomeMarcasMoto()
        {                        
            HomeMarcasMoto model = new HomeMarcasMoto
            {
                Marcas = MarcaService.Instance.GetMarcaByCatalogoID(eCommerceConstants.CATALOGO_MOTO_ID)                
            };
            return PartialView("HomeMarcasMoto", model);
        }

        [HttpPost]
        public ActionResult BuscarCategoria(int idCategoria, int idMarca) {
            return RedirectToAction("CatalogoMoto", "Home", new { categoryId = idCategoria, marcaId = idMarca });
        }

        //metodo para motos
        public ActionResult CatalogoMoto(int categoryId, int marcaId, string q, decimal? from, decimal? to, string sortby, int? pageNo, int? recordSize)
        {
            recordSize = recordSize ?? (int)RecordSizeEnums.Size20;

            ProductsViewModel model = new ProductsViewModel
            {
                Categories = CategoriesService.Instance.GetCategoryByCatalogoID(eCommerceConstants.CATALOGO_MOTO_ID),
                Marcas = MarcaService.Instance.GetMarcaByCatalogoID(eCommerceConstants.CATALOGO_MOTO_ID),
                CategoryID = 0,
                MarcaID = 0
            };

            if (categoryId > 0)
            {
                var selectedCategory = CategoriesService.Instance.GetCategoryByID(categoryId);

                if (selectedCategory == null) return HttpNotFound();
                else
                {
                    model.CategoryID = selectedCategory.ID;
                    model.CategoryName = selectedCategory.SanitizedName.ToUpper();
                    model.SelectedCategory = selectedCategory;

                    model.SearchedCategories = CategoryHelpers.GetAllCategoryChildrens(selectedCategory, model.Categories);
                }
            }

            if (marcaId > 0)
            {
                var selectedMarca = MarcaService.Instance.GetMarcaByID(marcaId);

                if (selectedMarca == null) return HttpNotFound();
                else
                {
                    model.MarcaID = selectedMarca.ID;
                    model.MarcaName = selectedMarca.Descripcion.ToUpper();
                    model.SelectedMarca = selectedMarca;
                }
            }

            model.SearchTerm = q;
            model.PriceFrom = from;
            model.PriceTo = to;
            model.SortBy = sortby;
            model.PageSize = recordSize;
            model.CatalogoID = eCommerceConstants.CATALOGO_MOTO_ID;

            var selectedCategoryIDs = model.SearchedCategories != null ? model.SearchedCategories.Select(x => x.ID).ToList() : null;

            model.Products = ProductsService.Instance.SearchProductsMoto(selectedCategoryIDs, eCommerceConstants.CATALOGO_MOTO_ID, marcaId, model.SearchTerm, model.PriceFrom, model.PriceTo, model.SortBy, pageNo, recordSize.Value, activeOnly: true, out int count, stockCheckCount: null);

            model.Products = getProductWithoutDolar(model.Products, model.PriceFrom, model.PriceTo);

            model.Pager = new Pager(count, pageNo, recordSize.Value);

            return View(model);
        }

        public ActionResult CatalogoParts(int categoryId, int marcaId, string q, decimal? from, decimal? to, string sortby, int? pageNo, int? recordSize)
        {
            recordSize = recordSize ?? (int)RecordSizeEnums.Size20;

            ProductsViewModel model = new ProductsViewModel
            {
                Categories = CategoriesService.Instance.GetCategoryByCatalogoID(eCommerceConstants.CATALOGO_ACCESORIO_ID),
                Marcas = MarcaService.Instance.GetMarcaByCatalogoID(eCommerceConstants.CATALOGO_ACCESORIO_ID),
                CategoryID = 0,
                MarcaID = 0
            };

            if (categoryId > 0)
            {
                var selectedCategory = CategoriesService.Instance.GetCategoryByID(categoryId);

                if (selectedCategory == null) return HttpNotFound();
                else
                {
                    model.CategoryID = selectedCategory.ID;
                    model.CategoryName = selectedCategory.SanitizedName.ToUpper();
                    model.SelectedCategory = selectedCategory;

                    model.SearchedCategories = CategoryHelpers.GetAllCategoryChildrens(selectedCategory, model.Categories);
                }
            }

            if (marcaId > 0)
            {
                var selectedMarca = MarcaService.Instance.GetMarcaByID(marcaId);

                if (selectedMarca == null) return HttpNotFound();
                else
                {
                    model.MarcaID = selectedMarca.ID;
                    model.MarcaName = selectedMarca.Descripcion.ToUpper();
                    model.SelectedMarca = selectedMarca;
                }
            }

            model.SearchTerm = q;
            model.PriceFrom = from;
            model.PriceTo = to;
            model.SortBy = sortby;
            model.PageSize = recordSize;
            model.CatalogoID = eCommerceConstants.CATALOGO_ACCESORIO_ID;

            var selectedCategoryIDs = model.SearchedCategories != null ? model.SearchedCategories.Select(x => x.ID).ToList() : null;

            model.Products = ProductsService.Instance.SearchProductsParts(selectedCategoryIDs, eCommerceConstants.CATALOGO_ACCESORIO_ID, marcaId, model.SearchTerm, model.PriceFrom, model.PriceTo, model.SortBy, pageNo, recordSize.Value, activeOnly: true, out int count, stockCheckCount: null);

            model.Products = getProductWithoutDolar(model.Products, model.PriceFrom, model.PriceTo);

            model.Pager = new Pager(count, pageNo, recordSize.Value);

            //Vista: CatalogoAccesory
            return View(model);            
        }

        public ActionResult Search(string category, string q, decimal? from, decimal? to, string sortby, int? pageNo, int? recordSize)
        {
            recordSize = recordSize ?? (int)RecordSizeEnums.Size20;

            ProductsViewModel model = new ProductsViewModel
            {
                Categories = CategoriesService.Instance.GetCategories()
            };

            if (!string.IsNullOrEmpty(category))
            {
                var selectedCategory = CategoriesService.Instance.GetCategoryByName(category);

                if (selectedCategory == null) return HttpNotFound();
                else
                {
                    model.CategoryID = selectedCategory.ID;
                    model.CategoryName = selectedCategory.SanitizedName;
                    model.SelectedCategory = selectedCategory;

                    model.SearchedCategories = CategoryHelpers.GetAllCategoryChildrens(selectedCategory, model.Categories);
                }
            }

            model.SearchTerm = q;
            model.PriceFrom = from;
            model.PriceTo = to;
            model.SortBy = sortby;
            model.PageSize = recordSize;

            var selectedCategoryIDs = model.SearchedCategories != null ? model.SearchedCategories.Select(x => x.ID).ToList() : null;

            model.Products = ProductsService.Instance.SearchProducts(selectedCategoryIDs, model.SearchTerm, model.PriceFrom, model.PriceTo, model.SortBy, pageNo, recordSize.Value, activeOnly: true, out int count, stockCheckCount: null);

            model.Pager = new Pager(count, pageNo, recordSize.Value);

            return View(model);
        }
        
        public ActionResult PriceRangeFilter(decimal? priceFrom, decimal? priceTo)
        {
            var model = new PriceRangeFilterViewModel
            {
                PriceFrom = priceFrom,
                PriceTo = priceTo,

                MaxPrice = ProductsService.Instance.GetMaxProductPrice()
            };

            return PartialView("SearchFilters/_PriceRangeFilter", model);
        }

        public ActionResult MotoPriceRangeFilter(decimal? priceFrom, decimal? priceTo)
        {                        
            int precioMaximo = (int) Math.Ceiling(ProductsService.Instance.GetMaxProductPrice());

            var model = new PriceRangeFilterViewModel
            {
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                MaxPrice = ProductsService.Instance.GetMaxProductPrice()
            };

            return PartialView("Catalogo/MotoFilters/_MotoPriceRangeFilter", model);
        }

        public List<Product> getProductWithoutDolar(List<Product> products, decimal? priceFrom, decimal? priceTo)
        {

            List<Product> ProductWithoutDolar = new List<Product>();
            decimal tipoCambio = TipoCambioService.Instance.GetTypeUltimateChanged();

            foreach (Product p in products)
            {
                //Moneda es Dolar?
                if (p.TipoMoneda.Equals(2))
                {
                    var DiscountTemp = p.Discount * tipoCambio;
                    if (priceFrom.HasValue && priceTo.HasValue)
                    {
                        if (DiscountTemp >= priceFrom && DiscountTemp <= priceTo)
                        {
                            ProductWithoutDolar.Add(p);
                        }
                    }
                    else
                    {
                        ProductWithoutDolar.Add(p);
                    }
                }
                else
                {
                    ProductWithoutDolar.Add(p);
                }
            }
            return ProductWithoutDolar;
        }

        [ValidateAntiForgeryToken]
        public JsonResult SubscribeNewsLetter(string email)
        {
            JsonResult jsonResult = new JsonResult();

            var newsletterSubscription = new NewsletterSubscription()
            {
                EmailAddress = email,
                IsVerified = false,
                UserID = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty,
                ModifiedOn = DateTime.Now,
                IsActive = true
            };

            var result = SharedService.Instance.SaveNewsletterSubscription(newsletterSubscription);

            if (result)
            {
                jsonResult.Data = new
                {
                    Success = true,
                    Message = "PP.Footer.NewsLetter.NewsletterSubscribed".LocalizedString()
                };
            }
            else
            {
                jsonResult.Data = new
                {
                    Success = false,
                    Message = "PP.Footer.NewsLetter.NewsletterAlreadySubscribed".LocalizedString()
                };
            }

            return jsonResult;
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SubmitContactForm(string subject, string name, string email, string message)
        {
            JsonResult jsonResult = new JsonResult();

            try
            {
                
                //send order placed notification email to admin emails
                await new EmailService()
                                 .SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName,
                                                   ConfigurationsHelper.SendGrid_FromEmailAddress,
                                                   ConfigurationsHelper.AdminEmailAddress,
                                                   EmailTextHelpers.ContactMessageSubject_Admin(),
                                                   EmailTextHelpers.ContactMessageBody_Admin(subject, name, email, message));

                jsonResult.Data = new
                {
                    Success = true,
                    Message = "Your message has been submitted. We will contact you back soon."
                };
            }
            catch (Exception)
            {
                jsonResult.Data = new
                {
                    Success = false,
                    Message = "An error occured while submitting your message."
                };
            }

            return jsonResult;
        }


        public ActionResult Nosotros()
        {
            return View();
        }


    }
}