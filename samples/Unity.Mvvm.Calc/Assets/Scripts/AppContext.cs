﻿using System;
using System.Collections.Generic;
using Interfaces;
using MathOperations;
using Models;
using UnityEngine;
using UnityMvvmToolkit.Core.Converters.ParameterConverters;
using UnityMvvmToolkit.Core.Converters.ValueConverters;
using UnityMvvmToolkit.Core.Interfaces;
using ViewModels;

public class AppContext : MonoBehaviour, IAppContext
{
    private Dictionary<Type, object> _registeredTypes;

    public void Construct()
    {
        _registeredTypes = new Dictionary<Type, object>();

        RegisterInstance(GetMathOperations());
        RegisterInstance(GetValueConverters());
        RegisterInstance(new FloatToStrConverter());

        RegisterInstance(new CalcModel(this));
        RegisterInstance(new CalcViewModel(this));
    }

    public T Resolve<T>()
    {
        return (T) _registeredTypes[typeof(T)];
    }

    private void RegisterInstance<T>(T instance)
    {
        _registeredTypes.Add(typeof(T), instance);
    }

    private IConverter[] GetValueConverters()
    {
        return new IConverter[]
        {
            new ParameterToStrConverter()
        };
    }

    private IReadOnlyDictionary<char, IMathOperation> GetMathOperations()
    {
        return new Dictionary<char, IMathOperation>
        {
            { '+', new AddOperation() },
            { '÷', new DivideOperation() },
            { '×', new MultiplyOperation() },
            { '−', new SubtractOperation() }
        };
    }
}