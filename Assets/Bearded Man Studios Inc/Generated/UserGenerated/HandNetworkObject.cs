using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0.25,0]")]
	public partial class HandNetworkObject : NetworkObject
	{
		public const int IDENTITY = 2;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private bool _active;
		public event FieldEvent<bool> activeChanged;
		public Interpolated<bool> activeInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool active
		{
			get { return _active; }
			set
			{
				// Don't do anything if the value is the same
				if (_active == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_active = value;
				hasDirtyFields = true;
			}
		}

		public void SetactiveDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_active(ulong timestep)
		{
			if (activeChanged != null) activeChanged(_active, timestep);
			if (fieldAltered != null) fieldAltered("active", _active, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.25f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Color _color;
		public event FieldEvent<Color> colorChanged;
		public Interpolated<Color> colorInterpolation = new Interpolated<Color>() { LerpT = 0f, Enabled = false };
		public Color color
		{
			get { return _color; }
			set
			{
				// Don't do anything if the value is the same
				if (_color == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_color = value;
				hasDirtyFields = true;
			}
		}

		public void SetcolorDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_color(ulong timestep)
		{
			if (colorChanged != null) colorChanged(_color, timestep);
			if (fieldAltered != null) fieldAltered("color", _color, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			activeInterpolation.current = activeInterpolation.target;
			positionInterpolation.current = positionInterpolation.target;
			colorInterpolation.current = colorInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _active);
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _color);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_active = UnityObjectMapper.Instance.Map<bool>(payload);
			activeInterpolation.current = _active;
			activeInterpolation.target = _active;
			RunChange_active(timestep);
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_color = UnityObjectMapper.Instance.Map<Color>(payload);
			colorInterpolation.current = _color;
			colorInterpolation.target = _color;
			RunChange_color(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _active);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _color);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (activeInterpolation.Enabled)
				{
					activeInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					activeInterpolation.Timestep = timestep;
				}
				else
				{
					_active = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_active(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (colorInterpolation.Enabled)
				{
					colorInterpolation.target = UnityObjectMapper.Instance.Map<Color>(data);
					colorInterpolation.Timestep = timestep;
				}
				else
				{
					_color = UnityObjectMapper.Instance.Map<Color>(data);
					RunChange_color(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (activeInterpolation.Enabled && !activeInterpolation.current.UnityNear(activeInterpolation.target, 0.0015f))
			{
				_active = (bool)activeInterpolation.Interpolate();
				//RunChange_active(activeInterpolation.Timestep);
			}
			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (colorInterpolation.Enabled && !colorInterpolation.current.UnityNear(colorInterpolation.target, 0.0015f))
			{
				_color = (Color)colorInterpolation.Interpolate();
				//RunChange_color(colorInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public HandNetworkObject() : base() { Initialize(); }
		public HandNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public HandNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
