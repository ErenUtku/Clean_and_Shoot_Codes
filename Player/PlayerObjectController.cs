using System;
using System.Collections.Generic;
using Controllers.Data;
using Controllers.State;
using FluffyUnderware.DevTools.Extensions;
using Lean.Transition;
using Models;
using UnityEngine;

namespace Player
{
    public class PlayerObjectController : MonoBehaviour
    {
        [SerializeField] private GameObject modelContainer;

        public List<Model> _modelList;
        private void OnEnable()
        {
            if(_modelList.Count==0)
            {
                CreateModelList();
            }

            ActivateCorrectModel(PlayerDataType.Hose);
        }

        private void Start()
        {
            CollectState.CollectStateActive += ModelContainerActivation;

            DataManager.OnDataChanged += ActivateCorrectModel;
        }


        private void OnDestroy()
        {
            CollectState.CollectStateActive -= ModelContainerActivation;
            DataManager.OnDataChanged -= ActivateCorrectModel;
        }

        private void CreateModelList()
        {
            _modelList = new List<Model>();
            
            foreach (Transform model in modelContainer.transform)
            {
                if (model.GetComponent<Model>() == null)
                    continue;

                _modelList.Add(model.GetComponent<Model>());
            }

        }

        private void ActivateCorrectModel(PlayerDataType dataType)
        {
            if (!modelContainer.activeSelf || dataType!=PlayerDataType.Hose) return;
            
            var hoseLevel = DataManager.Instance.Hose;
                
            foreach (var model in _modelList)
            {
                model.gameObject.SetActive(false);
                
                if (hoseLevel >= model.GetModelData().minValue && hoseLevel <= model.GetModelData().maxValue)
                {
                    model.gameObject.SetActive(true);
                }
            }
            
            
            
        }

        private void ModelContainerActivation(bool value)
        {
            modelContainer.SetActive(value);
        }
    }
}
