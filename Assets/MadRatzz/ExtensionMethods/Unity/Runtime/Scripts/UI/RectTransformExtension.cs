using System;
using UnityEngine;

namespace MadRatz.Extensions
{
	public enum AnchorType
	{
		TopLeft,
		TopCenter,
		TopRight,
		TopStretch,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		MiddleStretch,
		BottomLeft,
		BottomCenter,
		BottomRight,
		BottomStretch,
		StretchLeft,
		StretchCenter,
		StretchRight,
		StretchFill
	}

	public static class RectTransformExtension
	{
		public static void SetRectTransformTopAndBottomAnchors(this RectTransform rectTransform, float top,
		                                                       float bottom)
		{
			rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
			rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, top);
		}

		public static void SetRectTransformRectAnchors(this RectTransform rectTransform, Rect rect)
		{
			rectTransform.offsetMin = new Vector2(rect.xMin, rect.yMin);
			rectTransform.offsetMax = new Vector2(rect.xMax, rect.yMax);
		}

		public static void SetRectTransformRectLeftAndRightAnchors(this RectTransform rectTransform, float left,
		                                                           float right)
		{
			rectTransform.offsetMin = new Vector2(left, rectTransform.offsetMin.y);
			rectTransform.offsetMax = new Vector2(right, rectTransform.offsetMax.y);
		}

		public static void AnchorToCorners(this RectTransform transform)
		{
			if (transform == null)
				throw new ArgumentNullException(nameof(transform));

			if (transform.parent == null)
				return;

			RectTransform parent = transform.parent.GetComponent<RectTransform>();

			Rect rect = parent.rect;
			Vector2 newAnchorsMin = new(transform.anchorMin.x + transform.offsetMin.x / rect.width,
				transform.anchorMin.y + transform.offsetMin.y / rect.height);

			Vector2 newAnchorsMax = new(transform.anchorMax.x + transform.offsetMax.x / rect.width,
				transform.anchorMax.y + transform.offsetMax.y / rect.height);

			transform.anchorMin = newAnchorsMin;
			transform.anchorMax = newAnchorsMax;
			transform.offsetMin = transform.offsetMax = new Vector2(0, 0);
		}

		public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
		{
			trans.pivot = aVec;
			trans.anchorMin = aVec;
			trans.anchorMax = aVec;
		}


		public static float GetWidth(this RectTransform trans)
		{
			return trans.rect.width;
		}

		public static float GetHeight(this RectTransform trans)
		{
			return trans.rect.height;
		}

	

		public static void SetBottomLeftPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x + trans.pivot.x * trans.rect.width,
				newPos.y + trans.pivot.y * trans.rect.height, trans.localPosition.z);
		}

		public static void SetTopLeftPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x + trans.pivot.x * trans.rect.width,
				newPos.y - (1f - trans.pivot.y) * trans.rect.height, trans.localPosition.z);
		}

		public static void SetBottomRightPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x - (1f - trans.pivot.x) * trans.rect.width,
				newPos.y + trans.pivot.y * trans.rect.height, trans.localPosition.z);
		}

		public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
		{
			trans.localPosition = new Vector3(newPos.x - (1f - trans.pivot.x) * trans.rect.width,
				newPos.y - (1f - trans.pivot.y) * trans.rect.height, trans.localPosition.z);
		}

	

		public static void SetScale(this RectTransform trans, float scale)
		{
			Vector3 localScale = trans.localScale;
			localScale.x = scale;
			localScale.y = scale;
			localScale.z = scale;
			trans.localScale = localScale;
		}

		public static void SetAnchorAndPositionAsStretched(this RectTransform rectTransform)
		{
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.anchorMin = new Vector2(0, 0);
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
			rectTransform.rect.Set(0, 0, 0, 0);
			rectTransform.localScale = Vector3.one;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.localPosition = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
		}

		#region AnchoredPosition

		public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
		{
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.x = x;
			rectTransform.anchoredPosition = anchoredPosition;
		}

		public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
		{
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.y = y;
			rectTransform.anchoredPosition = anchoredPosition;
		}
		public static void SetAnchoredPositionXY(this RectTransform trans, Vector2 pos)
		{
			// Vector2 position = trans.anchoredPosition;
			// position.x = pos.x;
			// position.y = pos.y;
			// trans.anchoredPosition = position;
			SetAnchoredPositionX(trans, pos.x);
			SetAnchoredPositionY(trans, pos.y);
		}

		#endregion

		#region AnchoredPosition3D

		public static void SetAnchoredPosition3DX(this RectTransform rectTransform, float x)
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			anchoredPosition3D.x = x;
			rectTransform.anchoredPosition3D = anchoredPosition3D;
		}

		public static void SetAnchoredPosition3DY(this RectTransform rectTransform, float y)
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			anchoredPosition3D.y = y;
			rectTransform.anchoredPosition3D = anchoredPosition3D;
		}

		public static void SetAnchoredPosition3DZ(this RectTransform rectTransform, float z)
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			anchoredPosition3D.z = z;
			rectTransform.anchoredPosition3D = anchoredPosition3D;
		}

		#endregion

		#region Size

		//Deprecated
		// public static void SetSize(this RectTransform trans, Vector2 newSize)
		// {
		// 	Vector2 oldSize = trans.rect.size;
		// 	Vector2 deltaSize = newSize - oldSize;
		// 	Vector2 pivot;
		// 	trans.offsetMin =
		// 		trans.offsetMin - new Vector2(deltaSize.x * (pivot = trans.pivot).x, deltaSize.y * pivot.y);
		// 	trans.offsetMax = trans.offsetMax +
		// 	                  new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - pivot.y));
		// }

		// public static void SetWidth(this RectTransform trans, float newSize)
		// {
		// 	SetSize(trans, new Vector2(newSize, trans.rect.size.y));
		// }
		//
		// public static void SetHeight(this RectTransform trans, float newSize)
		// {
		// 	SetSize(trans, new Vector2(trans.rect.size.x, newSize));
		// }
		
		public static Vector2 GetSize(this RectTransform rectTransform)
		{
			return rectTransform.rect.size;
		}

		public static void SetSize(this RectTransform rectTransform, float width, float height)
		{
			rectTransform.SetWidth(width);
			rectTransform.SetHeight(height);
		}

		public static void SetSize(this RectTransform rectTransform, Vector2 size)
		{
			rectTransform.SetWidth(size.x);
			rectTransform.SetHeight(size.y);
		}

		public static void SetWidth(this RectTransform rectTransform, float width)
		{
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		}

		public static void SetHeight(this RectTransform rectTransform, float height)
		{
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
		}

		#endregion

		#region Corner

		// 1  2
		// 0  3
		public static Vector3[] GetCorners(this RectTransform rectTransform, bool isWorldSpace = true)
		{
			var corners = new Vector3[4];

			if (isWorldSpace)
				rectTransform.GetWorldCorners(corners);
			else
				rectTransform.GetLocalCorners(corners);

			return corners;
		}

		public static Vector3 GetLeftBottomCorner(this RectTransform rectTransform, bool isWorldSpace = true)
		{
			var corners = rectTransform.GetCorners(isWorldSpace);
			return corners[0];
		}

		public static Vector3 GetLeftTopCorner(this RectTransform rectTransform, bool isWorldSpace = true)
		{
			var corners = rectTransform.GetCorners(isWorldSpace);
			return corners[1];
		}

		public static Vector3 GetRightTopCorner(this RectTransform rectTransform, bool isWorldSpace = true)
		{
			var corners = rectTransform.GetCorners(isWorldSpace);
			return corners[2];
		}

		public static Vector3 GetRightBottomCorner(this RectTransform rectTransform, bool isWorldSpace = true)
		{
			var corners = rectTransform.GetCorners(isWorldSpace);
			return corners[3];
		}

		#endregion

		#region Anchor

		public static void SetAnchor(this RectTransform rectTransform, AnchorType anchorType)
		{
			switch (anchorType)
			{
				case AnchorType.TopLeft:
					rectTransform.SetAnchor(0f, 1f);
					break;
				case AnchorType.TopCenter:
					rectTransform.SetAnchor(0.5f, 1f);
					break;
				case AnchorType.TopRight:
					rectTransform.SetAnchor(1f, 1f);
					break;
				case AnchorType.TopStretch:
					rectTransform.SetAnchor(0f, 1f, 1f, 1f);
					break;
				case AnchorType.MiddleLeft:
					rectTransform.SetAnchor(0f, 0.5f);
					break;
				case AnchorType.MiddleCenter:
					rectTransform.SetAnchor(0.5f, 0.5f);
					break;
				case AnchorType.MiddleRight:
					rectTransform.SetAnchor(1f, 0.5f);
					break;
				case AnchorType.MiddleStretch:
					rectTransform.SetAnchor(0f, 0.5f, 1f, 0.5f);
					break;
				case AnchorType.BottomLeft:
					rectTransform.SetAnchor(0f, 0f);
					break;
				case AnchorType.BottomCenter:
					rectTransform.SetAnchor(0.5f, 0f);
					break;
				case AnchorType.BottomRight:
					rectTransform.SetAnchor(1f, 0f);
					break;
				case AnchorType.BottomStretch:
					rectTransform.SetAnchor(0f, 0f, 1f, 0f);
					break;
				case AnchorType.StretchLeft:
					rectTransform.SetAnchor(0f, 0f, 0f, 1f);
					break;
				case AnchorType.StretchCenter:
					rectTransform.SetAnchor(0.5f, 0f, 0.5f, 1f);
					break;
				case AnchorType.StretchRight:
					rectTransform.SetAnchor(1f, 0f, 1f, 1f);
					break;
				case AnchorType.StretchFill:
					rectTransform.SetAnchor(0f, 0f, 1f, 1f);
					break;
			}
		}

		public static void SetAnchor(this RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
		{
			rectTransform.anchorMin = new Vector2(minX, minY);
			rectTransform.anchorMax = new Vector2(maxX, maxY);
		}

		public static void SetAnchor(this RectTransform rectTransform, float x, float y)
		{
			rectTransform.SetAnchor(x, y, x, y);
		}

		public static void SetAnchorX(this RectTransform rectTransform, float anchorPosX)
		{
			Vector2 anchorPos = rectTransform.anchoredPosition;
			anchorPos.x = anchorPosX;
			rectTransform.anchoredPosition = anchorPos;
		}

		public static void SetAnchorY(this RectTransform rectTransform, float anchorPosY)
		{
			Vector2 anchorPos = rectTransform.anchoredPosition;
			anchorPos.y = anchorPosY;
			rectTransform.anchoredPosition = anchorPos;
		}

		#endregion

		#region Stretch

		public static void SetStretchFill(this RectTransform rectTransform, float padding = 0)
		{
			rectTransform.SetAnchor(AnchorType.StretchFill);
			rectTransform.SetStretch(padding, padding, padding, padding);
		}

		public static void SetStretch(this RectTransform rectTransform, float paddingTop, float paddingBottom,
		                              float paddingLeft, float paddingRight)
		{
			rectTransform.offsetMin = new Vector2(paddingLeft, paddingBottom);
			rectTransform.offsetMax = -new Vector2(paddingRight, paddingTop);
		}

		public static void SetHorizontalStretch(this RectTransform rectTransform, float paddingLeft, float paddingRight)
		{
			rectTransform.offsetMin = new Vector2(paddingLeft, rectTransform.offsetMin.y);
			rectTransform.offsetMax = new Vector2(paddingRight, rectTransform.offsetMax.y);
		}

		public static void SetVerticalStretch(this RectTransform rectTransform, float paddingTop, float paddingBottom)
		{
			rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, paddingBottom);
			rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, paddingTop);
		}

		public static void SetLeft(this RectTransform rectTransform, float paddingLeft)
		{
			rectTransform.offsetMin = new Vector2(paddingLeft, rectTransform.offsetMin.y);
		}

		public static void SetRight(this RectTransform rectTransform, float paddingRight)
		{
			rectTransform.offsetMax = new Vector2(paddingRight, rectTransform.offsetMax.y);
		}

		public static void SetTop(this RectTransform rectTransform, float paddingTop)
		{
			rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, paddingTop);
		}

		public static void SetBottom(this RectTransform rectTransform, float paddingBottom)
		{
			rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, paddingBottom);
		}

		#endregion
	}
}