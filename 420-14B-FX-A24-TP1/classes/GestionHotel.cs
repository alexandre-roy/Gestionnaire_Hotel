using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _420_14B_FX_A23_TP1.classes;

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
        public Chambre[] Chambre
        {
            get { return _chambres; }
            set { _chambres = value; }
        }

        #endregion

        #region CONSTRUCTEURS

        GestionHotel(string cheminFichierChambres, string cheminFichierReservations) 
        {
            

        }

        #endregion

        #region MÉTHODES

        private void ChargerChambres(string cheminFichierChambres)
        {
            string[] vectLignes = Utilitaire.ChargerDonnees(cheminFichierChambres);
            {
                for (int i = 0; i < vectLignes.Length; i++)
                {
                    Chambre[i] = vectLignes[i];
                }
        }

        #endregion
    }
}
