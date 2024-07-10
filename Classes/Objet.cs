using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Objet : BindableObject
{
    public string Nom { get; set; }

    public Objet()
    {
        Nom = "";
    }

    public Objet(string nom)
    {
        Nom = nom;
    }
}
