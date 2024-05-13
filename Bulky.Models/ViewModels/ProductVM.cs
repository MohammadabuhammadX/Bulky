using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; } // we name it CategoryList ,because this what we name it in productController
    }
}


/*
 First we will think about what will be all the entity that we want for our product . which is product object,
so we create that property which will be product 
 2nd: what we want is an Ienumerable to hold the dropdown


View models are models that are specifically designed for a view , And the advantage that we get because of that ,is that 
a view will be strongly typed to one model ,And because view models are also Known as strongly typed views
 */
