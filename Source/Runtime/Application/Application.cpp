#include "Application.hpp"
#include <iostream>

bool Application::Start()
{
    platform.dataPath = executableName + "Data/";
    if(!platform.Init(applicationName))
    {
        return false;
    }
    
    std::cout<<"\nStarted " + applicationName + "\n";
    std::cout<<"Created by: " + creatorName + "\n";
    std::cout<<"Enigma Runtime version: " + runtimeVersion + "\n";
    running = true;
    return true;
}

void Application::Update()
{
    SDL_Event e;
    /*while (SDL_PollEvent(&e))
    {
        if (e.type == SDL_QUIT)
        {
            running = false;
            return;
        }
    }*/
    
    platform.SetWindowTitle(platform.GetWindow(), std::to_string(frameCount));

    //platform.RenderImage();
    frameCount++;
}

void Application::FixedUpdate()
{

}

Application::~Application()
{
    if(running)
        platform.Quit();
}