using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHome.WebAPI
{
    public interface ICamera
    {
        void Initialize();
        Stream GetVideoAndAudioStream();
    }

    public interface ISensorScreen
    {
        event EventHandler<SensorScreenEventArgs> OnInputDataReceived;
    }

    public interface ILocker
    {
        void Lock();
        void Unlock();
        DoorState GetDoorState();
    }

    public interface IGateway
    {
        void Initialize();
        void Start();
    }

    public interface IIotHubClient
    {
        void SentMessage(IotHubMessage message);
        event EventHandler<IotHubMessageEventArgs> OnMessageReceived;
    }

    public interface IMediaService
    {
        Stream OpenStream(Guid id);
        bool CloseStream(Guid id);
    }

    public interface IRepository<TEntity>
    {
        TEntity[] GetAll();
        TEntity Get(Guid id);
        TEntity Get(Expression<Func<bool, TEntity>> filter);
        TEntity GetFirst(Expression<Func<bool, TEntity>> filter);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
