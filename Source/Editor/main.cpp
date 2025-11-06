#define SDL_main_HANDLED
#include <SDL.h>
#include <iostream>
#include <limits.h>

constexpr const char* ENGINE_VERSION = "No Enigma Runtime Found";
int n;

void StartupPrint()
{
    std::cout<<std::endl<<"Engine Version: "<<ENGINE_VERSION<<std::endl;
}

int main(int argc, char*argv[])
{
    StartupPrint();

    std::cout<<"Running SDL Window Test"<<std::endl;
    SDL_Window *window = NULL;
    if( SDL_Init(SDL_INIT_VIDEO != 0) )
    {
        std::cout<<std::endl<<"SDL failed to initialize";
        return 1;
    }

    window = SDL_CreateWindow("Enigma Editor", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, 0);
    
    if(window == NULL)
    {
        std::cout<<std::endl<<"SDL window failed to initialise";
        return 1;
    }

    SDL_Delay(6000);
    SDL_DestroyWindow(window);
    SDL_Quit();

    return 0;
}