﻿using System;

namespace RepositorioGitHub.Dominio.Entidades
{
    public class Favorite
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Login { get; set; }  
        public string Language { get; set; }
        public string Description { get; set; }
        public DateTime UpdateLast { get; set; }
    }
}
