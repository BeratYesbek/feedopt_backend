﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Core.Utilities.Language;
using Entity.Concretes.Translations;
using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace Entity.Concretes
{
    public class Color : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Hex { get; set; }

        public virtual List<ColorTranslation> ColorTranslations { get; set; }
    }
}
