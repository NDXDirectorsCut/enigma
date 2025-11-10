#include <SDL.h>
#include <SDL_opengl.h>
#include <iostream>
#include "Application/Application.h"

int n;

int main(int argc, char* argv[])
{ 
    if(!Init())
    {
        std::cout<<"Initialization Failed! \n";
        return 1;
    }

    RenderImage();

    SDL_Event e; bool quit = false; 
    while( quit == false )
    { 
        while( SDL_PollEvent( &e ) )
        {
            if( e.type == SDL_QUIT ) 
                quit = true; 
        } 
    }
    
    Quit();
    return 0;
}