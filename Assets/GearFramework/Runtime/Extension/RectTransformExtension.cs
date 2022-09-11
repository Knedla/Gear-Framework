using UnityEngine;

namespace GearFramework.Runtime
{
    public static class RectTransformExtension
    {
        public static void AlignRelativeTo(this RectTransform baseRectTransform, RectTransform rectTransform, AlignmentOption alignmentOption, int horizontalEdgeAlignmentOffset, int verticalEdgeAlignmentOffset)
        {
            if (alignmentOption == AlignmentOption.OppositeCorners_BottomRightFirst)
                baseRectTransform.AlignOppositeCorners_UpperLeftFirst(rectTransform, horizontalEdgeAlignmentOffset, verticalEdgeAlignmentOffset);
        }

        static void AlignOppositeCorners_UpperLeftFirst(this RectTransform baseRectTransform, RectTransform rectTransform, int horizontalEdgeAlignmentOffset, int verticalEdgeAlignmentOffset) // note: does not work properly if both x or both y are out off the screen
        {
            float placeAroundCenterX = rectTransform.position.x + rectTransform.rect.center.x * rectTransform.root.localScale.x;
            float placeAroundCenterY = rectTransform.position.y + rectTransform.rect.center.y * rectTransform.root.localScale.y;

            float placeAroundHalfWidth = rectTransform.sizeDelta.x * rectTransform.root.localScale.x / 2;
            float placeAroundHalfHeight = rectTransform.sizeDelta.y * rectTransform.root.localScale.y / 2;

            float x;
            float y;

            if (placeAroundCenterX - placeAroundHalfWidth - (baseRectTransform.sizeDelta.x * baseRectTransform.root.localScale.x) >= verticalEdgeAlignmentOffset)
                x = placeAroundCenterX - placeAroundHalfWidth - (baseRectTransform.rect.xMax * baseRectTransform.root.localScale.x);
            else
                x = placeAroundCenterX + placeAroundHalfWidth - (baseRectTransform.rect.xMin * baseRectTransform.root.localScale.x);

            if (placeAroundCenterY + placeAroundHalfHeight + (baseRectTransform.sizeDelta.y * baseRectTransform.root.localScale.y) <= Screen.height - horizontalEdgeAlignmentOffset)
                y = placeAroundCenterY + placeAroundHalfHeight - (baseRectTransform.rect.yMin * baseRectTransform.root.localScale.y);
            else
                y = placeAroundCenterY - placeAroundHalfHeight - (baseRectTransform.rect.yMax * baseRectTransform.root.localScale.y);

            baseRectTransform.position = new Vector3(x, y);
        }
    }
}
