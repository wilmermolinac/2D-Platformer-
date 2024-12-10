using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// Gestiona las armas del agente, incluyendo la visibilidad, selección y eventos relacionados.
/// </summary>
namespace WeaponSystem
{
    public class AgentWeaponManager : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer; // Referencia al componente que renderiza el sprite del arma.

        private WeaponStorage _weaponStorage; // Almacén para gestionar las armas disponibles.

        public UnityEvent<Sprite> OnWeaponSwap; // Evento que se activa al cambiar de arma.
        public UnityEvent OnMultipleWeapons; // Evento que se activa al tener múltiples armas.
        public UnityEvent OnWeaponPickUp; // Evento que se activa al recoger un arma.

        /// <summary>
        /// Inicializa el almacenamiento de armas y el sprite renderer.
        /// </summary>
        private void Awake()
        {
            _weaponStorage = new WeaponStorage(); // Inicializa el almacén de armas.

            spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el componente SpriteRenderer del agente.

            ToggleWeaponVisibility(false); // Oculta las armas al inicio.
        }

        /// <summary>
        /// Activa o desactiva la visibilidad del arma.
        /// </summary>
        /// <param name="isVisibility">Determina si el arma debe ser visible.</param>
        public void ToggleWeaponVisibility(bool isVisibility)
        {
            if (isVisibility)
            {
                // Cambia el sprite al del arma actual si la visibilidad está activada.
                SwapWeaponSprite(GetCurrentWeapon().weaponSprite);
            }

            // Activa o desactiva el SpriteRenderer.
            spriteRenderer.enabled = isVisibility;
        }

        /// <summary>
        /// Obtiene los datos del arma actual.
        /// </summary>
        /// <returns>Datos del arma actual.</returns>
        public WeaponData GetCurrentWeapon()
        {
            return _weaponStorage.GetCurrentWeapon();
        }

        /// <summary>
        /// Cambia el sprite del arma actual y activa el evento correspondiente.
        /// </summary>
        /// <param name="weaponSprite">Sprite del arma a mostrar.</param>
        private void SwapWeaponSprite(Sprite weaponSprite)
        {
            spriteRenderer.sprite = weaponSprite; // Asigna el nuevo sprite al SpriteRenderer.
            OnWeaponSwap?.Invoke(weaponSprite); // Invoca el evento de cambio de arma.
        }

        /// <summary>
        /// Cambia al siguiente arma disponible.
        /// </summary>
        public void SwapWeapon()
        {
            if (_weaponStorage.WeaponCount <= 0)
            {
                return; // No realiza ningún cambio si no hay armas disponibles.
            }

            // Cambia al siguiente arma en el almacén.
            SwapWeaponSprite(_weaponStorage.SwapWeapon().weaponSprite);
        }

        /// <summary>
        /// Añade un arma al almacén y actualiza la visibilidad si es necesario.
        /// </summary>
        /// <param name="weaponData">Datos del arma a añadir.</param>
        public void AddWeaponData(WeaponData weaponData)
        {
            bool isWeaponAdd = _weaponStorage.addWeaponData(weaponData); // Añade el arma al almacén.
            if (!isWeaponAdd)
                return; // No realiza ninguna acción si el arma ya existe.

            if (_weaponStorage.WeaponCount == 2)
            {
                OnMultipleWeapons?.Invoke(); // Activa el evento de múltiples armas.
            }

            // Actualiza el sprite del arma recogida.
            SwapWeaponSprite(weaponData.weaponSprite);
        }

        /// <summary>
        /// Recoge un arma y activa el evento de recogida.
        /// </summary>
        /// <param name="weaponData">Datos del arma recogida.</param>
        public void PickUpWeapon(WeaponData weaponData)
        {
            AddWeaponData(weaponData); // Añade el arma al almacén.
            OnWeaponPickUp?.Invoke(); // Activa el evento de recogida de arma.
        }

        /// <summary>
        /// Comprueba si se puede usar el arma actual, dependiendo del estado del personaje.
        /// </summary>
        /// <param name="isGrounded">Indica si el personaje está en el suelo.</param>
        /// <returns>Verdadero si el arma puede ser usada; de lo contrario, falso.</returns>
        public bool CanIUseWeapon(bool isGrounded)
        {
            if (_weaponStorage.WeaponCount <= 0)
            {
                return false; // No se puede usar el arma si no hay armas en el almacén.
            }

            return _weaponStorage.GetCurrentWeapon().CanBeUsed(isGrounded); // Comprueba si el arma actual es utilizable.
        }

        /// <summary>
        /// Obtiene una lista de los nombres de las armas del jugador.
        /// </summary>
        /// <returns>Lista de nombres de las armas del jugador.</returns>
        public List<string> GetPlayerWeaponsNames()
        {
            return _weaponStorage.GetPlayerWeaponNames();
        }
    }
}
