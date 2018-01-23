using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Common.Models;
using Prism.Events;

namespace CookBook.App.Infrastructure.Events
{
    public class UpdateRecipeEvent : PubSubEvent<RecipeDetailDto> {}
}
