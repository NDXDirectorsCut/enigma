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
        std::string applicationName = "Enigma Application";
        std::string creatorName = "";
        std::string executableName = "";
        std::string runtimeVersion = "";
    private:
        Platform platform;
        int frameCount = 0;
        bool running = false;
};

#endif