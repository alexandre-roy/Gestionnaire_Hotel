using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using _420_14B_FX_A23_TP1.classes;
using _420_14B_FX_A24_TP1.enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _420_14B_FX_A24_TP1.classes
{
    public class GestionHotel
    {

        #region ATTRIBUTS

        /// <summary>
        /// Vecteur de chambres
        /// </summary>
        private Chambre[] _chambres;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient la liste des chambres
        /// </summary>
        public Chambre[] Chambres
        {
            get { return _chambres; }
            set { _chambres = value; }
        }

              

        #endregion


        #region CONSTRUCTEURS

        public GestionHotel(string cheminFichierChambres, string cheminFichierReservations) 
        {
            ChargerChambres(cheminFichierChambres);
            
        }

        #endregion

        #region MÉTHODES

        private void ChargerChambres(string cheminFichierChambres)
        {
            string[] vectLignes = Utilitaire.ChargerDonnees(cheminFichierChambres);

            Chambres = new Chambre[vectLignes.Length];          

            for (int i = 1; i < vectLignes.Length; i++)
            {
                string[] detailsChambre = vectLignes[i].Split(';');

                ushort numero = ushort.Parse(detailsChambre[0]);
                decimal prix = decimal.Parse(detailsChambre[2]);
                TypeChambre type = (TypeChambre)Enum.Parse(typeof(TypeChambre), detailsChambre[1]);

                Chambre Chambre = new Chambre(numero, prix, type);

                Chambres[i] = Chambre;
            }
        }

        //private Chambre[] AjouterChambre(Chambre chambre, Chambre[] vectChambres)
        //{
        //    Chambre[] chambre1 = new Chambre[vectChambres.Length + 1];

        //    Chambre nouvelleChambre = new Chambre(311, 3, (TypeChambre)1);

        //    for (int i = 0; i < chambre1.Length; i++)
        //    {
        //        chambre1[i] = Chambres[i];

        //        chambre1[chambre1.Length] = nouvelleChambre;
        //    }

        //    return chambre1;
        //}

        //private Chambre[] RechercherChambresDisponibles()
        //{
        //    return Chambres;
        //}

        #endregion
    }
}


    