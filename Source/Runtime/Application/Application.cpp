#include <SDL.h>
#include <SDL_opengl.h>
#include <iostream>
#include "Application.h"

const int DEFAULT_WIDTH = 640;
const int DEFAULT_HEIGHT = 480;
constexpr const char* APPLICATION_NAME = "Enigma Application";

SDL_Window* window = NULL;
SDL_GLContext = glContext;

///

bool InitEnigma()
{
    if(SDL_Init(SDL_INIT_VIDEO) < 0 )
    {
        std::cout<<"Failed to initialize SDL. \n Error: "<<SDL_GetError() );
        return false;
    }

    window = SDL_CreateWindow(APPLICATION_NAME, SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, DEFAULT_WIDTH, DEFAULT_HEIGHT, SDL_WINDOW_SHOWN);
    if(window == NULL)
    {
        std::cout<<"Failed to create Window. \n Error: "<<SDL_GetError() ); 
    }
}

void Quit()
{
    SDL_DestroyWindow(window);
    window = NULL;
    SDL_Quit();
}