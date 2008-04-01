﻿using System;
////using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
using System.Collections.Generic;
using System.Diagnostics;

#if (XNA)
using Microsoft.Xna.Framework;
#endif

using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;

namespace FarseerGames.FarseerPhysics.Dynamics
{
    public class FluidDragController : Controller
    {
        AABB aabb;
        List<Geom> geomList;
        float density = 0;
        float linearDragCoefficient = 0;
        float rotationalDragCoeficient = 0;
        Vector2 gravity = Vector2.Zero;
        Vertices vertices;

        float totalArea = 0;
        float area = 0;
        Vector2 centroid = Vector2.Zero;
        Vector2 buoyancyForce = Vector2.Zero;
        Vector2 linearDragForce = Vector2.Zero;
        float rotationalDragTorque = 0;        

        public FluidDragController() {
            geomList = new List<Geom>();
        }

        public void Initialize(AABB aabb, float density, float linearDragCoeficient, float rotationalDragCoeficient, Vector2 gravity)
        {
            this.aabb = aabb;
            this.density = density;
            this.linearDragCoefficient = linearDragCoeficient;
            this.rotationalDragCoeficient = rotationalDragCoeficient;
            this.gravity = gravity;
            vertices = new Vertices();           
        }

        public void AddGeom(Geom geom)
        {
            geomList.Add(geom);
        }

        public override void Validate()
        {
            //do nothing
        }

        Vector2 totalForce;
        public override void ApplyForce(float dt)
        {
            for (int i = 0; i < geomList.Count; i++)
            {
                totalArea = geomList[i].localVertices.GetArea();
                if (!AABB.Intersect(geomList[i].aabb, aabb)) continue;
                FindVerticesInFluid(geomList[i]);
                if (vertices.Count < 3) continue;

                area = vertices.GetArea();
                if (area < .01) continue;

                centroid = vertices.GetCentroid(area);

                CalculateBuoyancy();

                CalculateDrag(geomList[i]);

                Vector2.Add(ref buoyancyForce, ref linearDragForce, out totalForce);
                geomList[i].body.ApplyForceAtWorldPoint(ref totalForce, ref centroid);

                geomList[i].body.ApplyTorque(rotationalDragTorque); 
            }         
        }

        Vector2 vert;
        private void FindVerticesInFluid(Geom geom)
        {
            vertices.Clear();
            for (int i = 0; i < geom.worldVertices.Count; i++)
            {
                vert = geom.worldVertices[i];
                if (aabb.Contains(ref vert ))
                {
                    vertices.Add(vert);
                }
            }
        }

        private void CalculateAreaAndCentroid()
        {
            area = vertices.GetArea();
            
            centroid = vertices.GetCentroid(area);
        }

        private void CalculateBuoyancy()
        {
            buoyancyForce = -gravity * area * density;            
        }


        float centroidSpeed;
        Vector2 centroidVelocity;        
        Vector2 axis = Vector2.Zero;
        float min;
        float max;
        float dragArea = 0;
        float unitCentroidVelocity;
        float partialMass = 0;
        Vector2 localCentroid = Vector2.Zero;
        private void CalculateDrag(Geom geom)
        {
            //localCentroid = geom.body.GetLocalPosition(centroid);
            geom.body.GetVelocityAtWorldPoint(ref centroid, out centroidVelocity);
            centroidSpeed = centroidVelocity.Length();
            centroidVelocity.Normalize();
            
            axis.X = -centroidVelocity.Y;
            axis.Y = centroidVelocity.X;
            axis.Normalize();

            vertices.ProjectToAxis(ref axis, out min, out max);

            dragArea = Math.Abs(max - min);

            partialMass = geom.body.mass * (area / totalArea);
            linearDragForce = -.5f*density*(centroidSpeed) * dragArea*linearDragCoefficient * partialMass * centroidVelocity;
            //dragForce = -.5f * density * (centroidSpeed * centroidSpeed) * dragArea * dragCoeficent * centroidVelocity;

            rotationalDragTorque = -geom.body.angularVelocity*rotationalDragCoeficient*partialMass;
       }        
    }















}
