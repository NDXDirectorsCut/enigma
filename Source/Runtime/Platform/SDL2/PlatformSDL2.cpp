#include <PlatformSDL2.hpp>

bool PlatformSDL2::Init(const std::string &appName)
{
    if(SDL_Init(SDL_INIT_VIDEO) < 0)
    {
        std::cout<<"Failed to initialize SDL! \n";
        std::cout<<"SDL Error: "<<SDL_GetError();
        return false
    }
    layer = "SDL2";

    CreateSurface(appName,640,480);
}

Surface PlatformSDL2::CreateSurface(const std::string &title, int width, int height)
{
    SDL_Window *window = SDL_CreateWindow
    (
        title.c_str(),
        SDL_WINDOWPOS_UNDEFINED,
        SDL_WINDOWPOS_UNDEFINED,
        width,
        height,
        SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE
    );

    if(window == NULL)
    {
        std::cout<<"Failed to create Surface: "<<title<<"\n";
        std::cout<<"SDL Error: "<<SDL_GetError();
        return false;
    }

    SDL_Renderer *renderer = SDL_CreateRenderer
    (
        window,
        SDL_RENDERER_ACCELERATED
    );

    Surface *surface = new Surface;
    surface->handle = window;
    surface->width = width;
    surface->height = height;

    surfaces.push_back(surface);

    return surface;
}
/*
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

    return true;
}

bool Platform::SetWindowTitle(SDL_Window* window, std::string windowTitle)
{
    SDL_SetWindowTitle(window, windowTitle.c_str());
    return true;
}

void Platform::Quit()
{
    if (texture) SDL_DestroyTexture(texture);
    if (renderer) SDL_DestroyRenderer(renderer);
    if (window) SDL_DestroyWindow(window);
    SDL_Quit();
}
*/