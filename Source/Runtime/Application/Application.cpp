#include <SDL.h>
#include <SDL_opengl.h>
#include <iostream>
#include "Application.h"

const int DEFAULT_WIDTH = 800;
const int DEFAULT_HEIGHT = 600;
char* APPLICATION_NAME = "Enigma Application";

SDL_Window* window = NULL;
SDL_Surface* screenSurface = NULL;
SDL_Surface* mediaSurface = NULL;
SDL_GLContext glContext;

///

bool Init()
{
    if(SDL_Init(SDL_INIT_VIDEO) < 0 )
    {
        std::cout<<"Failed to initialize SDL. \n Error: "<<SDL_GetError();
        return false;
    }

    window = SDL_CreateWindow(APPLICATION_NAME, SDL_WINDOWPOS_UNDEFINED,
            SDL_WINDOWPOS_UNDEFINED, DEFAULT_WIDTH, DEFAULT_HEIGHT, SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE );
    if(window == NULL)
    {
        std::cout<<"Failed to create Window. \n Error: "<<SDL_GetError(); 
        return false;
    }
    return true;
}

bool RenderImage()
{
    SDL_Surface* optimizedSurface = NULL;

    screenSurface = SDL_GetWindowSurface(window);
    mediaSurface = SDL_LoadBMP("LoadImage/imagetest.bmp");
    if(mediaSurface == NULL)
    {
        std::cout<<"Failed to load image \n Error: "<<SDL_GetError();
        return false;
    }
    optimizedSurface = SDL_ConvertSurface( mediaSurface, screenSurface->format, 0);
    SDL_FreeSurface(mediaSurface);

    SDL_Rect stretchRect;
    stretchRect.x = 0; stretchRect.y = 0;
    stretchRect.w = DEFAULT_WIDTH; stretchRect.h = DEFAULT_HEIGHT;
    SDL_BlitScaled(optimizedSurface, NULL, screenSurface, &stretchRect);
    SDL_UpdateWindowSurface(window);
    return true;
}

void Quit()
{
    SDL_DestroyWindow(window);
    window = NULL;
    SDL_Quit();
}