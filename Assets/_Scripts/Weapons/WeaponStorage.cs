using System.Collections.Generic;
using System.Linq;

namespace WeaponSystem
{
    /// <summary>
    /// Clase para gestionar el almacenamiento y manipulación de armas.
    /// </summary>
    public class WeaponStorage
    {
        /// <summary>
        /// Lista de datos de las armas disponibles.
        /// </summary>
        private List<WeaponData> _weaponDataList = new List<WeaponData>();

        /// <summary>
        /// Índice del arma actualmente seleccionada.
        /// </summary>
        private int _currentWeaponIndex = -1;

        /// <summary>
        /// Obtiene la cantidad de armas almacenadas.
        /// </summary>
        public int WeaponCount
        {
            get => _weaponDataList.Count;
        }

        /// <summary>
        /// Obtiene el arma actualmente seleccionada.
        /// </summary>
        /// <returns>Datos del arma seleccionada o null si no hay armas.</returns>
        public WeaponData GetCurrentWeapon()
        {
            if (_currentWeaponIndex == -1) // Si no hay un arma seleccionada.
            {
                return null; // Retorna null indicando que no hay arma activa.
            }

            return _weaponDataList[_currentWeaponIndex]; // Retorna el arma seleccionada.
        }

        /// <summary>
        /// Cambia al siguiente arma en la lista.
        /// </summary>
        /// <returns>El arma seleccionada después del cambio.</returns>
        public WeaponData SwapWeapon()
        {
            if (_currentWeaponIndex == -1) // Si no hay armas.
                return null;

            _currentWeaponIndex++; // Incrementa el índice actual.

            if (_currentWeaponIndex >= _weaponDataList.Count) // Si supera el límite de la lista.
            {
                _currentWeaponIndex = 0; // Reinicia al primer arma.
            }

            return _weaponDataList[_currentWeaponIndex]; // Retorna el arma seleccionada.
        }

        /// <summary>
        /// Agrega un arma nueva a la lista si no está ya incluida.
        /// </summary>
        /// <param name="weaponData">Datos del arma a agregar.</param>
        /// <returns>True si se agregó correctamente, false si ya existía.</returns>
        public bool addWeaponData(WeaponData weaponData)
        {
            if (_weaponDataList.Contains(weaponData)) // Comprueba si ya existe el arma.
                return false;

            _weaponDataList.Add(weaponData); // Agrega el arma a la lista.
            _currentWeaponIndex = _weaponDataList.Count - 1; // Selecciona la última arma agregada.

            return true;
        }

        /// <summary>
        /// Obtiene los nombres de todas las armas almacenadas.
        /// </summary>
        /// <returns>Lista de nombres de armas.</returns>
        public List<string> GetPlayerWeaponNames()
        {
            return _weaponDataList.Select(weapon => weapon.name).ToList(); // Devuelve los nombres de las armas.
        }
    }
}