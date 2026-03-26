#ifndef PLATFORM_SDL2_H
#define PLATFORM_SDL2_H

#include <Platform.hpp>
#include <SDL.h>

class PlatformSDL2 : public Platform
{
    public:
        PlatformSDL2();
        ~PlatformSDL2();

        bool Init(const std::string &initName);
        bool Quit();
        Surface CreateSurface(const std::string& title, int width = 640, int height = 480);
        bool DestroySurface(Surface *surface);

        void SetSurfaceTitle(Surface *surface, std::string title);
        void SetSurfaceSize(Surface *surface, int width, int height);
}

#endif