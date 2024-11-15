﻿using _420_14B_FX_A24_TP1.enums;

namespace _420_14B_FX_A24_TP1.classes
{   
    /// <summary>
    /// Représente une chambre dans le système de gestion d'un hôtel.
    /// </summary>
    public class Chambre
    {
        #region ATTRIBUTS

        /// <summary>
        /// Le numéro de la chambre.
        /// </summary>
        private ushort _numero;

        /// <summary>
        /// Le pris pour chaque nuit qu'on réserve cette chammbre.
        /// </summary>
        private decimal _prixParNuit;

        /// <summary>
        /// Le type de la chambre
        /// </summary>
        private TypeChambre _type;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient ou définit le numéro de la chambre.
        /// </summary>
        public ushort Numero
        {
            get { return _numero; }
            set {
                //Todo : Ajouter la validation
                if (value != 0)
                {
                    _numero = value;
                }       
            }
        }

        /// <summary>
        /// Obtient ou définit le type de la chambre (par exemple, Simple, Double, Suite).
        /// </summary>
        public TypeChambre Type
        {
            get { return _type; }
            set {
                if (Enum.IsDefined(typeof(TypeChambre), value))
                {
                    _type = value;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le prix par nuit de la chambre.
        /// </summary>
        public decimal PrixParNuit
        {
            get { return _prixParNuit; }
            set {
                if (value >= 0)
                {
                    _prixParNuit = value;
                }
            }
        }

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Initialise une nouvelle instance de la classe Chambre.
        /// </summary>
        /// <param name="numero"> Numéro de la chambre. </param>
        /// <param name="prixParNuit"> Prix par nuit de la chamrbe. </param>
        /// <param name="type"> Type de la chambre. </param>
        public Chambre(ushort numero, decimal prixParNuit, TypeChambre type)
        {
            Numero = numero;
            PrixParNuit = prixParNuit;
            Type = type;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Retourne une chaîne de caractères représentant l'objet Chambre.
        /// </summary>
        /// <returns> Une chaîne de caractères représentant les informations de la chambre. </returns>
        public override string ToString()
        {
            string numeroPadRight = $"{Numero}".PadRight(11, ' ');
            string typePadRight = $"{Type}".PadRight(20, ' ');

            return $"{numeroPadRight}{typePadRight}{PrixParNuit}";
        }

        #endregion
    }
}
