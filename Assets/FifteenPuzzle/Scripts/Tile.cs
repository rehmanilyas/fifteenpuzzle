using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] TextMesh _text;

        Vector3 startPos;
        Vector3 targetPos;
        SpriteRenderer _renderer;

        public Vector3 TargetPosition
        {
            get { return targetPos; }
        }

        void Start()
        {
            startPos = transform.position;
            targetPos = startPos;
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetNumber(int num)
        {
            _text.text = num.ToString();
        }

        public bool IsInPlace()
        {
            return targetPos.Equals(startPos);
        }

        public void MoveToPos(Vector3 newPos)
        {
            targetPos = newPos;

            StartCoroutine(MoveToTarget());
        }

        public void MoveToPosNoAnim(Vector3 newPos)
        {
            targetPos = newPos;
            transform.position = targetPos;

            ColorAdjustment();
        }

        IEnumerator MoveToTarget()
        {
            while(Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 10f);
                yield return null;
            }
            transform.position = targetPos;

            ColorAdjustment();
        }

        void ColorAdjustment()
        {
            _renderer.color = IsInPlace() ? Color.green : Color.white;
        }

        void OnMouseDown()
        {
            GameManager.Instance.ClickedTile(this);
        }
    }
}
