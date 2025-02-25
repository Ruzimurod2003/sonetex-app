﻿namespace SonetexApp.Models;
public class State                              // Гарантия  
{
    public int Id { get; set; }
    public string Name { get; set; }            // Hазвание 
    public string NameRussian { get; set; }     // Русский нaзвание 
    public string NameEnglish { get; set; }     // Английский название 
    public string NameUzbek { get; set; }       // Узбекский название 
    public List<Product> Products { get; set; } = new List<Product>();
}