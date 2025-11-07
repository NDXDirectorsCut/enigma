#include <SDL.h>
#include <SDL_opengl.h>
#include <iostream>
#include "Application/Application.h"

constexpr const char* ENGINE_VERSION = "No Enigma Runtime Found";
int n;

void StartupPrint()
{
    std::cout<<std::endl<<"Engine Version: "<<ENGINE_VERSION<<std::endl;
}

int main(int argc, char* argv[])
{
    StartupPrint();
    InitEnigma();
    return 0;
}