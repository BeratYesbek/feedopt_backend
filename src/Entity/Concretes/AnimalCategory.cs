﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entity.Abstracts;
using Core.Utilities.Language;
using Entity.Concretes;
using Entity.Concretes.Translations;


namespace Entity.concretes
{
    public class AnimalCategory : IEntity
    {
        public int Id { get; set; }
        public string AnimalCategoryName { get; set; }
        public List<Advert> Adverts { get; set; }
        public List<AnimalSpecies> AnimalSpecies { get; set; } 

        //private ICollection<AnimalCategoryTranslation> _animalCategoryTranslations;

        /*public virtual ICollection<AnimalCategoryTranslation> AnimalCategoryTranslations
        {
            get
            {
                if (_animalCategoryTranslations != null)
                    new Translate<AnimalCategoryTranslation>().TranslateProperties(_animalCategoryTranslations,this);
                return null;
            }
            set => _animalCategoryTranslations = value;
        }*/


    }
}