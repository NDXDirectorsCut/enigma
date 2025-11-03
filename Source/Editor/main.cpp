#include <SDL.h>
#include <iostream>

constexpr const char* EDITOR_VERSION = "0.0.1";
constexpr const char* ENGINE_VERSION = "No Enigma Runtime Found";
int n;

void StartupPrint()
{
    std::cout<<std::endl<<"Engine Version: "<<ENGINE_VERSION<<std::endl<<"Editor Version: "<<EDITOR_VERSION;
}

int main()
{
    StartupPrint();

    SDL_Window *window = NULL;
    if( SDL_Init(SDL_INIT_VIDEO != 0) )
    {
        std::cout<<std::endl<<"SDL failed to initialize";
        return 1;
    }

    window = SDL_CreateWindow("Enigma Editor", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 1920, 1080, 0);

    if(window == NULL)
    {
        std::cout<<std::endl<<"SDL window failed to initialise";
        return 1;
    }

    SDL_Delay(60);
    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}