#include "Platform.hpp"
#include <iostream>

///

bool Platform::Init(const std::string& windowTitle, int width, int height)
{
    screenWidth = width;
    screenHeight = height;

    if(SDL_Init(SDL_INIT_VIDEO) < 0 )
    {
        std::cout<<"Failed to initialize SDL. \n Error: "<<SDL_GetError();
        return false;
    }

    window = SDL_CreateWindow(
        windowTitle.c_str(),
        SDL_WINDOWPOS_UNDEFINED,
        SDL_WINDOWPOS_UNDEFINED,
        width,
        height,
        SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE );

    if(window == nullptr)
    {
        std::cout<<"Failed to create Window. \n Error: "<<SDL_GetError(); 
        return false;
    }

    
    renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
    if (!renderer)
    {
        std::cout << "Renderer error: " << SDL_GetError();
        return false;
    }

    SDL_Surface* surface = SDL_LoadBMP("hl3_demo.vpk");
    texture = SDL_CreateTextureFromSurface(renderer, surface);
    SDL_FreeSurface(surface);

    if (!texture)
    {
        std::cout << "Texture error: " << SDL_GetError();
        return false;
    }

    return true;
}

bool Platform::RenderImage()
{
    int w, h;
    SDL_GetWindowSize(window, &w, &h);

    SDL_Rect dst{0, 0, w, h};

    SDL_RenderClear(renderer);
    SDL_RenderCopy(renderer, texture, nullptr, &dst);
    SDL_RenderPresent(renderer);

    return true;
}

void Platform::Quit()
{
    if (texture) SDL_DestroyTexture(texture);
    if (renderer) SDL_DestroyRenderer(renderer);
    if (window) SDL_DestroyWindow(window);
    SDL_Quit();
}