using System;
using System.Collections.Generic;
using Controllers;
using Controllers.Data;
using TMPro;
using UnityEngine;
using Weapons;

public class WeaponPlate : MonoBehaviour
{
    [Header("Select Weapon")]
    [SerializeField] private WeaponType weaponType;
    
    [Space]
    
    [Header("Plate Weapon Details")]
    [SerializeField] private Transform weaponContainer;
    
    [Header("Plate Stars Details")]
    [SerializeField] private GameObject starContainer;
    [SerializeField] private Material collectedStarMaterial;
    
    [Header("Player Level Details")]
    [SerializeField] private TextMeshProUGUI weaponLevelText;

    [Header("PlateColoring")] 
    [SerializeField] private GameObject coloringPlate;
    [SerializeField] private Material correctPlateMaterial;
    
    
    //WeaponData
    private Weapon _selectedWeapon;
    private int _weaponLevel;
    private GameObject _createdWeapon;

    private void Awake()
    {
        DataManager.OnDataChanged += SetPlateColor;
    }

    private void OnDestroy()
    {
        DataManager.OnDataChanged -= SetPlateColor;
    }

    private void Start()
    {
        SelectedWeapon();

        CalculateWeaponStar();

        SetWeaponLevelText();

        SetPlateColor(PlayerDataType.Hose);
    }

    #region PRIVATE FIELDS

    private void SetWeaponLevelText()
    {
        weaponLevelText.text = $"Lvl. {_weaponLevel.ToString()}";
    }

    private void CalculateWeaponStar()
    {
        var starCount = GetStarCountForWeaponLevel();

        SetStarMaterials(starCount);
    }
    
    private int GetStarCountForWeaponLevel()
    {
        Dictionary<int, int> starRateMappings = new Dictionary<int, int>
        {
            { 1, 1 },    // Level 1 maps to 1 star
            { 5, 2 },    // Level 5 maps to 2 stars
            { 10, 3 }    // Level 10 maps to 3 stars
        };

        int starCount = 0;

        foreach (var mapping in starRateMappings)
        {
            if (_weaponLevel >= mapping.Key)
            {
                starCount = mapping.Value;
            }
            else
            {
                break;
            }
        }

        return starCount;
    }
    
    private void SetStarMaterials(int starCount)
    {
        int childCount = starContainer.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (i < starCount)
            {
                var star = starContainer.transform.GetChild(i);
                var starRenderer = star.GetComponent<Renderer>();
                starRenderer.material = collectedStarMaterial;
            }
            else
            {
                break;
            }
        }
    }

    private void SelectedWeapon()
    {
        _selectedWeapon = WeaponManager.Instance.WeaponByString(weaponType.ToString());

        if (_selectedWeapon != null)
        {
            _createdWeapon = Instantiate(_selectedWeapon.gameObject, weaponContainer.transform.position, Quaternion.identity);

            _weaponLevel = _selectedWeapon.GetWeaponData().level;
        }
        else
        {
            DestroyImmediate(this.gameObject);
            Debug.LogWarning($"Weapon Couldn't Find in {this}");
        }
        
    }
    
    private void SetPlateColor(PlayerDataType dataType)
    {
        if (dataType != PlayerDataType.Hose) return;
        
        var hoseLevel = DataManager.Instance.Hose;

        if (hoseLevel < _weaponLevel) return;
            
        var plateRenderer = coloringPlate.GetComponent<Renderer>();
        plateRenderer.material = correctPlateMaterial;

    }

    #endregion

    #region PUBLIC FIELDS

    public GameObject GetWeapon()
    {
        return _createdWeapon;
    }

    #endregion

   
}

public enum WeaponType
{
    Grenade,
    Hammer,
    Knife,
    P90,
    Pistol
}