using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CookBook.App.Infrastructure.Events;
using CookBook.Common.Interfaces;
using CookBook.Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace CookBook.App.Recipes.ViewModels
{
    public class RecipeListViewModel : BindableBase
    {
        private ObservableCollection<RecipeListDto> _recipes;
        private bool _isLoading = true;

        public RecipeListViewModel(ICookBookRepository cookBookRepository, IEventAggregator eventAggregator)
        {
            this.CookBookRepository = cookBookRepository;
            this.EventAggregator = eventAggregator;

            this.EventAggregator.GetEvent<UpdateRecipeEvent>().Subscribe(this.UpdateRecipe);
            this.Recipes = new ObservableCollection<RecipeListDto>();
            this.OnLoad().ConfigureAwait(false); 
        }
        
        private void UpdateRecipe(RecipeDetailDto recipeDetailDto)
        {
            this.Recipes.Add(new RecipeListDto()
            {
                Id = recipeDetailDto.Id,
                Name = recipeDetailDto.Name,
                Duration = recipeDetailDto.Duration,
                Type = recipeDetailDto.Type,
            });
        }

        private async Task OnLoad()
        {
            await Task.Run(async () =>
            {
                this.IsLoading = true;
                await Task.Delay(1000); //TODO Remove, Simulates DB delay
                this.Recipes = new ObservableCollection<RecipeListDto>(this.CookBookRepository.GetAllRecipes());
                this.IsLoading = false;
            });
        }

        public ObservableCollection<RecipeListDto> Recipes
        {
            get => this._recipes;
            set => this.SetProperty(ref this._recipes, value);
        }

        private ICookBookRepository CookBookRepository { get; }
        private IEventAggregator EventAggregator { get; }

        public ICommand SelectRecipeCommand => new DelegateCommand<RecipeListDto>(SelectRecipe);

        public bool IsLoading
        {
            get => this._isLoading;
            set => this.SetProperty(ref this._isLoading, value);
        }

        private void SelectRecipe(RecipeListDto recipeListModel)
        {
            this.EventAggregator.GetEvent<SelectedRecipeEvent>().Publish(recipeListModel);
        }
    }
}
