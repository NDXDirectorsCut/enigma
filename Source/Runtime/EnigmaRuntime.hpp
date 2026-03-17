#ifndef ENIGMA_RUNTIME_H
#define ENIGMA_RUNTIME_H

#pragma once
#include "Application/Application.hpp"


#ifdef _WIN32
    #ifdef ENIGMA_RUNTIME_EXPORT
        #define ENIGMA_API __declspec(dllexport)
    #else
        #define ENIGMA_API __declspec(dllimport)
    #endif
#else
    #define ENIGMA_API
#endif

#endif