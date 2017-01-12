using System;
using UnityEngine;
using Verse;
using RimWorld;
namespace GrowDesignators
{
    public class Designator_ZoneAdd_GrowingRich : Designator_ZoneAdd
    {
        protected override string NewZoneLabel
        {
            get
            {
                return "GrowingZone".Translate();
            }
        }

        public Designator_ZoneAdd_GrowingRich()
        {
            this.zoneTypeToPlace = typeof(Zone_Growing);
            this.defaultLabel = "Growing Zone - RichSoil";
            this.defaultDesc = "DesignatorGrowingZoneDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneCreate_Growing", true);
            this.hotKey = KeyBindingDefOf.Misc2;
            this.tutorTag = "ZoneAdd_Growing";
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c).Accepted)
            {
                return false;
            }
            if (base.Map.fertilityGrid.FertilityAt(c) < TerrainDef.Named("SoilRich").fertility)
            {
                return false;
            }
            return true;
        }

        protected override Zone MakeNewZone()
        {
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.GrowingFood, KnowledgeAmount.Total);
            return new Zone_Growing(Find.VisibleMap.zoneManager);
        }
    }
}
