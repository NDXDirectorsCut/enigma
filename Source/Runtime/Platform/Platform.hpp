#ifndef PLATFORM_H
#define PLATFORM_H

#include <string>

struct Surface
{
    void *handle;
    int width;
    int height;
};

class Platform
{
    public:
        virtual ~Platform()
        {
            Quit();
        }

        virtual bool Init(const std::string &initName) = 0;
        virtual void Quit() = 0;
        virtual Surface CreateSurface( const std::string& title, int width = 0, int height = 0) = 0;
        virtual void DestroySurface(Surface *surface) = 0;

        virtual void SetSurfaceTitle(Surface *surface, std::string title) = 0;
        virtual void SetSurfaceSize(Surface *surface, int width, int height) = 0;
        std::string dataPath = "";
        std::string layer; // Current Layer Used (ex: SDL2)
    private:
        std::vector<Surface*> surfaces;
};

#endif