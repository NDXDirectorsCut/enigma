#ifndef APPLICATION_H
#define APPLICATION_H

#pragma once
#include <string>
#include "../Platform/Platform.hpp"

class Application
{
    public:
        Application() = default;
        ~Application();
        bool Start();
        void Update();
        void FixedUpdate();
        bool IsRunning() const { return running; }   
    private:
        Platform platform;
        bool running = false;
};

#endif