#ifndef PLATFORM_H
#define PLATFORM_H

#pragma once
#include <string>
#include <SDL.h>

class Platform
{
    public:
        Platform() = default;
        ~Platform()
        {
            Quit();
        }

        bool Init(const std::string& windowTitle, int width = 800, int height = 600);
        bool SetWindowTitle(SDL_Window* window, std::string windowTitle);
        bool RenderImage();
        void Quit();

        SDL_Window* GetWindow() const { return window; }
        std::string dataPath = "";
        
    private:
        SDL_Window* window = nullptr;
        SDL_Renderer* renderer = nullptr;
        SDL_Texture* texture = nullptr;

        
        int screenWidth = 800;
        int screenHeight = 600;
};

#endif