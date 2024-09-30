﻿using System.Windows.Controls;
using _420_14B_FX_A24_TP1.enums;

namespace _420_14B_FX_A24_TP1.classes
{
    /// <summary>
    /// Représente une chambre dans le système de gestion d'un hôtel.
    /// </summary>
    public class Chambre
    {
        // Champs privés
        private ushort _numero;
        private decimal _prixParNuit;
        private TypeChambre _type;

        /// <summary>
        /// Obtient ou définit le numéro de la chambre.
        /// </summary>
        public ushort Numero
        {
            get { return _numero; }
            set {
                //Todo : Ajouter le formattage
                if (value > 0)
                _numero = value; 
            }
        }

        /// <summary>
        /// Obtient ou définit le type de la chambre (par exemple, Simple, Double, Suite).
        /// </summary>
        //Todo : Ajouter la propriété manquante
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
            set { _prixParNuit = value; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Chambre/>.
        /// </summary>


        //Todo : Implémenter le constructeur avec paramèttres
        public Chambre(ushort _numero, decimal _prixParNuit, TypeChambre _type)
        {
            Numero = _numero;
            PrixParNuit = _prixParNuit;
            Type = _type;
        }

        /// <summary>
        /// Retourne une chaîne de caractères représentant l'objet Chambre.
        /// </summary>
        /// <returns>
        /// Une chaîne de caractères représentant les informations de la chambre.
        /// </returns>
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
