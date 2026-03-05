#include "Application.hpp"
#include <iostream>

bool Application::Start()
{
    if(!platform.Init(applicationName))
    {
        return false;
    }
    running = true;
    return true;
}

void Application::Update()
{
    SDL_Event e;
    while (SDL_PollEvent(&e))
    {
        if (e.type == SDL_QUIT)
        {
            running = false;
            return;
        }
    }

    platform.RenderImage();
}

void Application::FixedUpdate()
{

}

Application::~Application()
{
    if(running)
        platform.Quit();
}