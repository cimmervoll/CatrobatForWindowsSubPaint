﻿using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.Core.Objects.Bricks
{
    public class PlaceAtBrick : Brick
    {
        protected Formula _xPosition;
        public Formula XPosition
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }

        protected Formula _yPosition;
        public Formula YPosition
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                RaisePropertyChanged();
            }
        }


        public PlaceAtBrick() {}

        public PlaceAtBrick(Sprite parent) : base(parent) {}

        public PlaceAtBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            _xPosition = new Formula(xRoot.Element("xPosition"));
            _yPosition = new Formula(xRoot.Element("yPosition"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("placeAtBrick");

            var xVariable1 = new XElement("xPosition");
            xVariable1.Add(_xPosition.CreateXML());
            xRoot.Add(xVariable1);

            var xVariable2 = new XElement("yPosition");
            xVariable2.Add(_yPosition.CreateXML());
            xRoot.Add(xVariable2);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PlaceAtBrick(parent);
            newBrick._xPosition = _xPosition.Copy(parent) as Formula;
            newBrick._yPosition = _yPosition.Copy(parent) as Formula;

            return newBrick;
        }
    }
}