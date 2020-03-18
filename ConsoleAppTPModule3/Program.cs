using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetLinq.BO;

namespace ConsoleAppTPModule3
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        static void Main(string[] args)
        {
            InitialiserDatas();

            //Question 1 :
            foreach(Auteur auteur in ListeAuteurs.Where(auteur => auteur.Nom.StartsWith("G")))
            {
                Console.WriteLine($"Question 1 : {auteur.Nom}");
            }

            //Question 2 :
            Console.WriteLine($"Question 2 : {ListeLivres.GroupBy(livre => livre.Auteur).OrderByDescending(grp => grp.Count()).First().Key.Nom}");

            //Question 3 :
            foreach (var item in ListeLivres.GroupBy(livre => livre.Auteur))
            {
                Console.WriteLine($"Question 3 : {item.Key.Nom} {item.Key.Prenom} {item.Average(livre => livre.NbPages)}");
            }

            //Question 4 :
            Console.WriteLine($"Question 4 : {ListeLivres.OrderByDescending(livre => livre.NbPages).First().Titre}");

            //Question 5 :
            Console.WriteLine($"Question 5 : {ListeAuteurs.Average(auteur => auteur.Factures.Sum(facture => facture.Montant))}");

            //Question 6 :
            foreach (var auteur in ListeAuteurs)
            {
                Console.WriteLine($"Question 6 : Auteur :{auteur.Nom} {auteur.Prenom} a écrie :");
                foreach(var livre in ListeLivres.Where(livre => livre.Auteur.Equals(auteur))) 
                {
                    Console.WriteLine(livre.Titre);
                }
            }

            //Question 7 :
            foreach (var livre in ListeLivres.OrderBy(livre => livre.Titre))
            {
                Console.WriteLine($"Question 7 : {livre.Titre}");
            }

            //Question 8 :
            var moyenne = ListeLivres.Average(livre => livre.NbPages);
            foreach(var livre in ListeLivres.Where(livre => livre.NbPages>moyenne))
            {
                Console.WriteLine($"Question 8 : {livre.Titre}");
            }


            //Question 9 :
            //Auteur ayant écrit le moin de livre parmis ce qui on écrit au moins 1 livre
            Console.WriteLine($"Question 9 (solution 1) : {ListeLivres.GroupBy(livre => livre.Auteur).OrderBy(grp => grp.Count()).FirstOrDefault().Key.Nom}");

            //Auteur ayant écrit le moin de livre parmis tous les auteur
            Dictionary<Auteur, int> monDico = new Dictionary<Auteur, int>();
            foreach (var auteur in ListeAuteurs)
            {
                monDico.Add(auteur, ListeLivres.GroupBy(livre => livre.Auteur).Count());
            }
            Console.WriteLine($"Question 9 (solution 2) : {monDico.OrderBy(dico => dico.Value).Last().Key.Nom}");

            //Autre solution
            Console.WriteLine($"Question 9 (solution 3) : {ListeAuteurs.OrderBy(auteur => ListeLivres.Count(livre => livre.Auteur == auteur)).FirstOrDefault().Nom}");


            Console.ReadKey();
        }


       
        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

    }
}
